/*
 * Licensed under the Apache License, Version 2.0 (http://www.apache.org/licenses/LICENSE-2.0)
 * See https://github.com/aspnet-contrib/AspNet.Security.OAuth.Providers
 * for more information concerning the license and the contributors participating to this project.
 */

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace AspNet.Security.OAuth.Meetup
{
    /// <summary>
    /// Defines a set of options used by <see cref="MeetupAuthenticationHandler"/>.
    /// </summary>
    public class MeetupAuthenticationOptions : OAuthOptions
    {
        public MeetupAuthenticationOptions()
        {
            AuthenticationScheme = MeetupAuthenticationDefaults.AuthenticationScheme;
            DisplayName = MeetupAuthenticationDefaults.DisplayName;
            ClaimsIssuer = MeetupAuthenticationDefaults.Issuer;

            CallbackPath = new PathString(MeetupAuthenticationDefaults.CallbackPath);

            AuthorizationEndpoint = MeetupAuthenticationDefaults.AuthorizationEndpoint;
            TokenEndpoint = MeetupAuthenticationDefaults.TokenEndpoint;
            UserInformationEndpoint = MeetupAuthenticationDefaults.UserInformationEndpoint;
        }
    }
}
