using Microsoft.Owin;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

[assembly: OwinStartup(typeof(Expense.WebApi.Hosting.IISHost.Startup))]
namespace Expense.WebApi.Hosting.IISHost
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            WebApiConfiguration.Configure(app);

            app.Run((context) =>
            {
                context.Response.ContentType = "text/plain";
                return context.Response.WriteAsync("Web Api started.");
            }); 
        }
    }
}