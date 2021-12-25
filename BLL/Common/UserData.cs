using System.Collections.Generic;
using System.Collections.Immutable;
using System.Security.Claims;

namespace NuxtTemplate.BLL.Common
{
    public class UserData
    {
        public UserData(ClaimsPrincipal principal)
        {
            Principal = principal;
            var permissions = ImmutableList.CreateBuilder<string>();
            IsAuthenticated = principal?.Identity.IsAuthenticated ?? false;
            if (IsAuthenticated)
                foreach (var claim in principal.Claims)
                    if (claim.Type == ClaimTypes.Name)
                        UserName = claim.Value;
                    else if (claim.Type == ClaimTypes.Email)
                        Email = claim.Value;
                    else if (claim.Type == ClaimTypes.Surname)
                        LastName = claim.Value;
                    else if (claim.Type == ClaimTypes.Role)
                        permissions.Add(claim.Value);
                    else if (claim.Type == "EmployeeKey")
                        EmployeeKey = int.TryParse(claim.Value, out var value) ? value : 0;
            Permissions = permissions.ToImmutable();
        }

        public bool IsAuthenticated { get; }
        public string UserName { get; } = "";
        public string Email { get; } = "";
        public string LastName { get; } = "";
        public IList<string> Permissions { get; }

        public IClaimsPrincipal Principal { get; }
        public int EmployeeKey { get; } = 0;
    }
}
