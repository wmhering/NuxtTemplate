using System;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

using NuxtTemplate.BLL.Common.Results;

namespace NuxtTemplate.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApplicationController : Controller
    {
        private IWebHostEnvironment _Env;

        public ApplicationController(IWebHostEnvironment env)
        {
            _Env = env;
        }

        [HttpGet("Environment")]
        public async Task<IActionResult> Environment()
        {
            var result = new DataResult<object>
            {
                Data = new {
                    ApiUrl = (new UriBuilder {
                        Scheme = Request.Scheme,
                        Host = Request.Host.Host,
                        Port = Request.Host.Port ?? 0,
                        Path = Request.PathBase.Add("/api").Value
                     }).ToString(),
                    EnvironmentName = _Env.EnvironmentName }
            };
            return Ok(result);
        }
    }
}
