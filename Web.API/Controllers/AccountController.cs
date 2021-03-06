using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using NuxtTemplate.BLL.Common;
using NuxtTemplate.BLL.Common.Results;

namespace NuxtTemplate.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        [HttpGet("User")]
        //[Authorize]
        public async Task<IActionResult> Data()
        {
            var result = new DataResult<UserData>();
            result.Data = new UserData(User);
            if (!result.Data.IsAuthenticated)
                result.Error("User is not authenticated");
            return Ok(result);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody]LoginData loginData)
        {
            var result = new DataResult<UserData>();
            if (ModelState.IsValid)
            {
                result.Messages.AddRange(ModelState.ResultMessages());
                return BadRequest(result);
            }
            if ((loginData.UserName ?? "").ToLower() != "admin@test.com" || loginData.Password != "123123")
            {
                result.Messages.Add(new ResultMessage { Status = ResultStatus.Error, Message = "" });
                return StatusCode(403, result);
            }
            SignIn(GetUserClaim(loginData.UserName));
            result.Data = new UserData(User);
            result.Messages.Add(new ResultMessage { Status = ResultStatus.Information, Message = "Everything is good!" });
            return Ok(result);
        }

        [HttpPost("Logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            SignOut();
            var result = new BLL.Common.Results.EmptyResult();
            result.Messages.Add(new ResultMessage { Status = ResultStatus.Information, Message = "Everything is good!" });
            return Ok(result);
        }

        private ClaimsPrincipal GetUserClaim(string userName)
        {
            var identity = new ClaimsIdentity();
            identity.AddClaims(new Claim[]
            {
                new Claim(ClaimTypes.Name, userName),
                new Claim(ClaimTypes.Email, userName),
                new Claim(ClaimTypes.Surname, "admin"),
                new Claim(ClaimTypes.Role, "administrator"),
                new Claim("EmployeeKey", "100")
            });
            return new ClaimsPrincipal(identity);
        }

        public class LoginData
        {
            [DisplayName("User Name")]
            [Required(AllowEmptyStrings = false)]
            public string UserName { get; set; }

            [Required(AllowEmptyStrings = false)]
            public string Password { get; set; }
        }
    }
}
