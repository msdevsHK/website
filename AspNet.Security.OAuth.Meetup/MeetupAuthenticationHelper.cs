/*
 * Licensed under the Apache License, Version 2.0 (http://www.apache.org/licenses/LICENSE-2.0)
 * See https://github.com/aspnet-contrib/AspNet.Security.OAuth.Providers
 * for more information concerning the license and the contributors participating to this project.
 */

using Newtonsoft.Json.Linq;

namespace AspNet.Security.OAuth.Meetup
{
    /// <summary>
    /// Contains static methods that allow to extract user's information from a <see cref="JObject"/>
    /// instance retrieved from Meetup after a successful authentication process.
    /// </summary>
    public static class MeetupAuthenticationHelper
    {
        /// <summary>
        /// Gets the identifier corresponding to the authenticated user.
        /// </summary>
        public static string GetIdentifier(JObject payload) => payload.Value<string>("id");

        /// <summary>
        /// Gets the display name corresponding to the authenticated user.
        /// </summary>
        public static string GetName(JObject payload) => payload.Value<string>("name");
    }
}
