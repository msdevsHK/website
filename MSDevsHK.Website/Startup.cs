using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using AspNet.Security.OAuth.Meetup;
using Microsoft.AspNetCore.Authentication.Cookies;
using MSDevsHK.Website.Data.DocumentDB;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace MSDevsHK.Website
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            Environment = env;

            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddAppEnvironmentVariables()
                .AddUserSecrets<Startup>();

            Configuration = builder.Build();
        }

        public IHostingEnvironment Environment { get; }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // The application uses cookies to handle sessions to know whether users are authorized to access the app.
            // Authentication challenges happen via the Meetup middleware.
            services.AddAuthentication(options =>
            {
                options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            });

            // Adds services required for using options.
            services.AddOptions();

            // Register the IConfiguration instance which options binds against.
            services.Configure<DocumentDBDataRepositoryOptions>(Configuration);

            // Add framework services.
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            // Set logging and error handling.

            loggerFactory.AddConsole(Configuration.GetSection("Logging"));

            if (env.IsDevelopment())
            {
                loggerFactory.AddDebug();
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            // Ensure that requests are always handled with HTTPS.
            var rewriteOptions = new RewriteOptions();
            // Use status 308 for permanent redirect while reusing the same HTTP method.
            rewriteOptions.AddRedirectToHttps(308, Environment.IsDevelopment() ? Program.DevHttpsPort : 443);
            app.UseRewriter(rewriteOptions);

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AutomaticAuthenticate = true,
                AutomaticChallenge = true,
                LoginPath = new PathString("/sign-on"),
                LogoutPath = new PathString("/sign-off")
            });

            // Use meetup for the authentication challenge, and handle the session with a cookie.
            app.UseMeetupAuthentication((MeetupAuthenticationOptions options) =>
            {
                options.CallbackPath = new PathString("/sign-on-meetup");
                options.ClientId = Configuration.GetValue("OAuth:Meetup:ClientId", "undefined-client-id");
                options.ClientSecret = Configuration.GetValue("OAuth:Meetup:ClientSecret", "undefined-client-secret");
            });

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
