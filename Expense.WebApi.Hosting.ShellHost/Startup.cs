using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expense.WebApi.Hosting.ShellHost
{
    public class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            WebApiConfiguration.Configure(appBuilder); 
        }
    }
}
