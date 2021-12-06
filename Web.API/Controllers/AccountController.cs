using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using NuxtTemplate.BLL.Results;

namespace NuxtTemplate.Web.Controllers
{
    [ApiController]
    [Route("Account")]
    public class AccountController : Controller
    {
        [HttpGet("User")]
        //[Authorize]
        public IActionResult Data()
        {
            var result = new DataResult<UserData>();
            result.Data = new UserData(User);
            result.Messages.Add(new ResultMessage { Status = ResultStatus.Information, Message = "Everything is good!" });
            return Ok(result);
        }

        [HttpPost("Login")]
        public IActionResult Login([FromBody]LoginData loginData)
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
            SignIn(GetUserClaim(loginData.UserName), null);
            result.Data = new UserData(User);
            result.Messages.Add(new ResultMessage { Status = ResultStatus.Information, Message = "Everything is good!" });
            return Ok(result);
        }

        [HttpPost("Logout")]
        [Authorize]
        public IActionResult Logout()
        {
            SignOut();
            var result = new BLL.Results.EmptyResult();
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

        public class UserData
        {
            public UserData(ClaimsPrincipal principal)
            {
                foreach (var claim in principal.Claims)
                    if (claim.Type == ClaimTypes.Name)
                        UserName = claim.Value;
                    else if (claim.Type == ClaimTypes.Email)
                        Email = claim.Value;
                    else if (claim.Type == ClaimTypes.Surname)
                        LastName = claim.Value;
                    else if (claim.Type == ClaimTypes.Role)
                        Permissions.Add(claim.Value);
                    else if (claim.Type == "EmployeeKey")
                        EmployeeKey = int.TryParse(claim.Value, out var value) ? (int?)value : null;
            }

            public string UserName { get; set; } = "";
            public string Email { get; set; } = "";
            public string LastName { get; set; } = "";
            public List<string> Permissions { get; } = new List<string>();
            public int? EmployeeKey { get; set; } = 0;

            public static UserData FromClaims(ClaimsPrincipal principal)
            {
                var result = new UserData(principal);;
                return result;
            }
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
