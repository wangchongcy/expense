using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expense.Model.Exceptions
{
    /// <summary>
    /// The exception is thrown when the text misses "<total></total>" property
    /// </summary>
    public class MissingTotalPropertyException : Exception
    {
    }
}
