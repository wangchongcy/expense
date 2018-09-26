using Owin;
using Expense.Infrastructure;
using Expense.Service;
using Expense.WebApi.Formatters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using Unity;
using Unity.Lifetime;

namespace Expense.WebApi
{
    public class WebApiConfiguration
    {
        /// <summary>
        /// Configure Web API
        /// </summary>
        /// <param name="appBuilder">Owin app builder</param>
        public static void Configure(IAppBuilder appBuilder)
        {
            HttpConfiguration httpConfiguration = new HttpConfiguration();

            ConfigureDependencyResolver(httpConfiguration);
            ConfigureFormatter(httpConfiguration);
            ConfigureExceptionHandler(httpConfiguration); 
            ConfigureRoute(httpConfiguration); 

            appBuilder.UseWebApi(httpConfiguration);
        }

        private static void ConfigureDependencyResolver(HttpConfiguration httpConfiguration)
        {
            var container = new UnityContainer();
            container.RegisterType<ILogger, Logger>(new ContainerControlledLifetimeManager());
            container.RegisterType<IExpenseService, ExpenseService>(new ContainerControlledLifetimeManager());
            httpConfiguration.DependencyResolver = new UnityDependencyResolver(container);
        }

        private static void ConfigureFormatter(HttpConfiguration httpConfiguration)
        {
            httpConfiguration.Formatters.Add(new PlainTextFormatter());
        }

        private static void ConfigureExceptionHandler(HttpConfiguration httpConfiguration)
        {
            httpConfiguration.Services.Replace(typeof(IExceptionHandler), new GlobalExceptionHandler()); 
        }

        private static void ConfigureRoute(HttpConfiguration httpConfiguration)
        {
            httpConfiguration.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
