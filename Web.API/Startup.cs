using System;
using System.Text.Json.Serialization;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using NuxtTemplate.BLL.Common;

namespace NuxtTemplate.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            Env = env;
        }

        public IConfiguration Configuration { get; }

        public IWebHostEnvironment Env { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers()
                .AddJsonOptions(opt => {
                    opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                    opt.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                });
            if (Env.IsDevelopment())
            {
                services.AddEndpointsApiExplorer();
                services.AddSwaggerGen();
            }
            services.AddHttpContextAccessor();
            services.AddScoped<UserData>(serviceProvider => {
                var contextAccessor = serviceProvider.GetRequiredService<HttpContextAccessor>();
                return new UserData(contextAccessor.HttpContext.User);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            //app.UseHttpsRedirection();
            if (Env.IsDevelopment())
                app
                    .UseCors(c =>
                    {
                        c.AllowAnyHeader()
                        .AllowAnyMethod()
                        .WithOrigins("http://localhost:3000", "https://localhost:3000")
                        .AllowCredentials();
                    })
                    .UseSwagger()
                    .UseSwaggerUI();

            app.UseVirtualFile("/uiConfig.js", ctx =>
            {
                var apiUrl = (new UriBuilder {
                    Scheme = ctx.Request.Scheme,
                    Host = ctx.Request.Host.Host,
                    Port = ctx.Request.Host.Port ?? -1,
                    Path = ctx.Request.PathBase.Add("/api").Value
                }).ToString();
                return $"export const apiURL = '{apiUrl}'\nexport const environmentName = '{Env.EnvironmentName}'\n";
            }, "application/javascript");
            app.UseStaticFiles();

            app.UseRouting();

            //app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
