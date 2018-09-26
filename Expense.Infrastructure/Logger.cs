using Microsoft.Practices.EnterpriseLibrary.Logging;
using Microsoft.Practices.EnterpriseLibrary.Logging.Formatters;
using Microsoft.Practices.EnterpriseLibrary.Logging.TraceListeners;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expense.Infrastructure
{
    public class Logger : ILogger
    {
        private LogWriter logWriter; 

        public Logger()
        {
            logWriter = new LogWriterFactory().Create();
        }

        public void Debug(string message)
        {
            logWriter.Write(message, "Debug"); 
        }

        public void Trace(string message)
        {
            logWriter.Write(message, "Trace");
        }

        public void Info(string message)
        {
            logWriter.Write(message, "Info"); 
        }

        public void Exception(Exception exception)
        {
            logWriter.Write(exception.ToString(), "Exception"); 
        }
    }
}
