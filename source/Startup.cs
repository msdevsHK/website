using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace MSDevsHK.Website
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app)
        {
            app.Run(context =>
            {
                context.Response.Headers.Add("Content-Type", new StringValues("text/html"));
                return context.Response.WriteAsync(
                    "<h1>Microsoft Developers HK</h1>" +
                    "<p>Hello from ASP.NET Core!</p>");
            });
        }
    }
}