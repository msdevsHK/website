using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNet.Security.OAuth.Meetup;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace MSDevsHK.Website.Controllers
{
    /// <summary>
    /// Handle authentication with an external authentication provider.
    /// </summary>
    /// <remarks>
    /// Based on https://github.com/aspnet-contrib/AspNet.Security.OAuth.Providers/blob/master/samples/Mvc.Client/Controllers/AuthenticationController.cs
    /// </remarks>
    public class AuthenticationController : Controller
    {
        [HttpGet("~/sign-on"), HttpPost("~/sign-on")]
        public IActionResult SignOn()
        {
            // Instruct the middleware corresponding to the requested external identity
            // provider to redirect the user agent to its own authorization endpoint.
            // Note: the authenticationScheme parameter must match the value configured in Startup.cs
            return Challenge(
                new AuthenticationProperties { RedirectUri = "/" },
                MeetupAuthenticationDefaults.AuthenticationScheme);
        }

        [HttpGet("~/sign-off"), HttpPost("~/sign-off")]
        public IActionResult SignOff()
        {
            // Instruct the cookies middleware to delete the local cookie created
            // when the user agent is redirected from the external identity provider
            // after a successful authentication flow (e.g Meetup).
            return SignOut(
                new AuthenticationProperties { RedirectUri = "/" },
                CookieAuthenticationDefaults.AuthenticationScheme);
        }
    }
}