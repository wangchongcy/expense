using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expense.Model.Exceptions
{
    /// <summary>
    /// The exception is thrown when xml open tag have no corresponding closing tag.
    /// </summary>
    public class XmlTagNotMatchingException : Exception
    {
    }
}
