using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace MSDevsHK.Website.Services
{
    public class AuthManager
    {
        public static async Task ValidatePrincipal(CookieValidatePrincipalContext context)
        {
            IEnumerable<AuthenticationToken> tokens = context.Properties.GetTokens();
            // TODO: Check access token for validity.
        }
    }
}