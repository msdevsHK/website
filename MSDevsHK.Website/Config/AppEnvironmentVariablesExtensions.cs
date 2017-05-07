using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace MSDevsHK.Website
{
    /// <summary>
    /// Extension methods for <see cref="IConfigurationBuilder"/> to load application specific environment variables.
    /// </summary>
    public static class AppEnvironmentVariablesExtensions
    {
        /// <summary>
        /// Reads application specific environment variables and assigns them to well known configuration entries.
        /// </summary>
        /// <param name="builder">The configuration builder to update.</param>
        /// <returns>The updated configuration builder.</returns>
        public static IConfigurationBuilder AddAppEnvironmentVariables(this IConfigurationBuilder builder)
        {
            var entries = new Dictionary<string, string>();

            string meetupClientId = Environment.GetEnvironmentVariable("APP_OAUTH_MEETUP_CLIENTID");
            if (meetupClientId != null) entries.Add("OAuth:Meetup:ClientId", meetupClientId);

            string meetupClientSecret = Environment.GetEnvironmentVariable("APP_OAUTH_MEETUP_CLIENTSECRET");
            if (meetupClientSecret != null) entries.Add("OAuth:Meetup:ClientSecret", meetupClientSecret);

            return builder.AddInMemoryCollection(entries);
        }
    }
}