using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expense.Infrastructure
{
    public interface ILogger
    {
        void Debug(string message);
        void Trace(string message);
        void Info(string message);
        void Exception(Exception exception);
    }
}
