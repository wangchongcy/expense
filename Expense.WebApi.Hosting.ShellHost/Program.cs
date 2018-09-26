using Microsoft.Owin.Hosting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expense.WebApi.Hosting.ShellHost
{
    class Program
    {
        // Hosting Web API
        static void Main(string[] args)
        {
            Console.WriteLine("Service is starting..."); 
            var startupUrl = ConfigurationManager.AppSettings["StartupUrl"];
            using (WebApp.Start<Startup>(url: startupUrl))
            {
                Console.WriteLine($"Service started. StartupUrl: {startupUrl}");

                Console.WriteLine("Press any key to exit...");
                Console.ReadLine();
            }
        }
    }
}
