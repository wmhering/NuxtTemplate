using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NuxtTemplate.Web
{
    public class VirtualFileMiddleware
    {
        private Func<HttpContext, string> _Content;
        private string _ContentType;
        private ILogger _Logger;
        private RequestDelegate _Next;
        private string _Path;

        public VirtualFileMiddleware(RequestDelegate next, ILogger<VirtualFileMiddleware> logger, string path, Func<HttpContext, string> content, string contentType)
        {
            _Next = next ?? throw new ArgumentNullException(nameof(next));
            _Logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _Path = path ?? throw new ArgumentNullException(nameof(path));
            _Content = content ?? throw new ArgumentNullException(nameof(content));
            _ContentType = contentType ?? throw new ArgumentNullException(nameof(contentType));
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var path = context.Request.Path;
            if(StringComparer.InvariantCultureIgnoreCase.Equals(path, _Path))
            {
                context.Response.Headers["Content-Type"] = _ContentType;
                var content = _Content.Invoke(context);
                await context.Response.WriteAsync(content);
                return;
            }
            await _Next.Invoke(context);
        }
    }
}

namespace Microsoft.AspNetCore.Builder
{
    public static class VirtualFileExtensions
    {
        public static IApplicationBuilder UseVirtualFile(this IApplicationBuilder builder, string path, Func<HttpContext, string> content, string contentType = "text/plain")
        {
            var logger = builder.ApplicationServices.GetService(typeof(ILogger<NuxtTemplate.Web.VirtualFileMiddleware>)) as ILogger<NuxtTemplate.Web.VirtualFileMiddleware>;
            return builder.UseMiddleware<NuxtTemplate.Web.VirtualFileMiddleware>(logger, path, content, contentType);
        }
    }
}
