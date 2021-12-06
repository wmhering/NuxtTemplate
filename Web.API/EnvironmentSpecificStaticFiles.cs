using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace NuxtTemplate.Web
{
    public class EnvironmentSpecificStaticFilesMiddleware
    {
        private string _EnvironmentName;
        private Dictionary<string, string> _FileNameMappings;
        private ILogger _Logger;
        private RequestDelegate _Next;

        public EnvironmentSpecificStaticFilesMiddleware(RequestDelegate next, ILogger<EnvironmentSpecificStaticFilesMiddleware> logger, IWebHostEnvironment env, string[] files)
        {
            _Next = next ?? throw new ArgumentNullException(nameof(next));
            _Logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _EnvironmentName = (env ?? throw new ArgumentNullException(nameof(env))).EnvironmentName;
            _FileNameMappings = new Dictionary<string, string>(files.Length, StringComparer.InvariantCultureIgnoreCase);
            if (string.IsNullOrWhiteSpace(_EnvironmentName)) // If no environment name then do nothing when invoked.
            {
                _Logger.LogError("EnvironmentName not available, files will not be mapped to environment specific versions. Verify that ASPNETCORE_ENVIRONMENT environment variable is set.");
                return;
            }
            BuildFileMappings(files ?? throw new ArgumentNullException(nameof(files)));
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // If the request is for a file with an environment specific version then add the environment name to the file name.
            var path = context.Request.Path;
            if (_FileNameMappings.ContainsKey(path))
                context.Request.Path = _FileNameMappings[path];
            // Let the StaticFilesMiddleware return the file content;
            await _Next(context);
        }

        private string AddEnvironmentNameBeforeExtension(string fileName)
        {
            var i = fileName.LastIndexOf(".");
            return $"{fileName.Substring(0, i)}.{_EnvironmentName}{fileName.Substring(i)}";
        }

        private string AddEnvironmentNameToEnd(string fileName)
        {
            return $"{fileName}.{_EnvironmentName}";
        }

        private void BuildFileMappings(string[] files)
        {
            foreach(var file in files)
            {
                if (string.IsNullOrWhiteSpace(file))
                {
                    _Logger.LogError("Cannot add environment name to file with no name.");
                    continue;
                }
                if (!file.StartsWith("/"))
                {
                    _Logger.LogError($"File names must start with initial '/'. Cannot add environment name to file '{file}'.");
                    continue;
                }
                var indexOfLastSeperator = file.LastIndexOf('/');
                var indexOfLastPeriod = file.LastIndexOf('.');
                _FileNameMappings[file] = indexOfLastPeriod < indexOfLastSeperator ? AddEnvironmentNameToEnd(file) : AddEnvironmentNameBeforeExtension(file);
            }
            if (_FileNameMappings.Count>0 && _Logger.IsEnabled(LogLevel.Information))
            {
                var filenames = string.Join(", ", _FileNameMappings.Keys.Select(s => $"'{s}'").ToArray());
                var index = filenames.LastIndexOf(", ");
                if (index > 0)
                    filenames = filenames.Substring(0, index) + " & " + filenames.Substring(index + 2);
                _Logger.LogInformation($"Adding EnvironmentName '{_EnvironmentName}' to file(s): {filenames}.");
            }
        }
    }
}

namespace Microsoft.AspNetCore.Builder
{
    public static class EnvironmentSpecificStaticFilesExtensions
    {
        public static IApplicationBuilder UseEnvironmentSpecificStaticFiles(this IApplicationBuilder builder, params string[] files)
        {
            var env = builder.ApplicationServices.GetService(typeof(IWebHostEnvironment)) as IWebHostEnvironment;
            var logger = builder.ApplicationServices.GetService(typeof(ILogger<NuxtTemplate.Web.EnvironmentSpecificStaticFilesMiddleware>)) as ILogger<NuxtTemplate.Web.EnvironmentSpecificStaticFilesMiddleware>;

            return builder.UseMiddleware<NuxtTemplate.Web.EnvironmentSpecificStaticFilesMiddleware>(logger, env, files);
        }
    }
}