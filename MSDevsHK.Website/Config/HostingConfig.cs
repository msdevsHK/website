using System;

namespace MSDevsHK.Website
{
    /// <summary>
    /// Hosting related application configuration.
    /// </summary>
    public class HostConfig
    {
        /// <summary>
        /// Gets the hostname of the website how it is accessed in the browser.
        /// </summary>
        /// <remarks>Exclude protocol and port, only the DNS/IP/Host name.</remarks>
        public string Hostname { get; set; }

        /// <summary>
        /// Gets the file path to the PFX file containing the SSL certificate for HTTPS connections.
        /// </summary>
        public string HttpsPfxPath { get; set; }

        /// <summary>
        /// Gets the password of the <see cref="HttpsPfxPath" /> to load the SSL certificate.
        /// </summary>
        public string HttpsPfxPassword { get; set; }
    }
}