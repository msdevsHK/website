using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.CommandLine;

namespace MSDevsHK.Website
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Determine whether we run under the development environment, to load specific host setup.
            string env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            bool isDevEnv = env?.Equals("Development", StringComparison.OrdinalIgnoreCase) == true;

            string cwd = Directory.GetCurrentDirectory();

            var config = new ConfigurationBuilder()
                .SetBasePath(cwd)
                .AddUserSecrets<Program>()
                .AddJsonFile("appsettings.json", optional: true)
                .AddJsonFile($"appsettings.{env}.json", optional: true)
                .Build();

            var hostConfig = config.GetSection("Host").Get<HostConfig>();

            var hostBuilder = new WebHostBuilder()
                .CaptureStartupErrors(true)
                .UseKestrel(options =>
                {
                    // Setup HTTPS with a development SSL certificate. Different environments should use a specific
                    // certificate.
                    if (isDevEnv)
                    {
                        string absoluteHttpsPfxPath = Path.Combine(cwd, hostConfig.HttpsPfxPath);
                        options.UseHttps(absoluteHttpsPfxPath, hostConfig.HttpsPfxPassword);
                    }
                })
                .UseContentRoot(cwd);

            if (isDevEnv)
            {
                // Use a specific host URL for local development, where the HTTPS URL matches the one in the SSL
                // certificate used in the development environment. The development SSL certificate prevents that the
                // browser would display any SSL security warnings.
                // The website URL must be configured on the machine `hosts` file to redirect the DNS lookup to the
                // localhost; 127.0.0.1.
                // Note that the below URLs do not bind the server to the default ports HTTP 80 and HTTPS 443 because
                // that would require administrative permissions on most OS'es.
                hostBuilder.UseUrls(
                    $"http://{hostConfig.Hostname}:8000",
                    $"https://{hostConfig.Hostname}:44300");
            }
            else
            {
                // No need to define UseUrls, as the IIS integration will setup listeners to handle HTTP and HTTPS
                // traffic automatically.
                hostBuilder.UseIISIntegration();
            }

            hostBuilder.UseStartup<Startup>();

            IWebHost host = hostBuilder.Build();
            host.Run();
        }
    }
}
