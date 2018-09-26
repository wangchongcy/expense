using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expense.Model
{
    /// <summary>
    /// Domian model for a expense property information, which contains name/value pair. 
    /// </summary>
    public class ExpenseProperty
    {
        public string Name { get; set; }

        public string Value { get; set; }
    }
}
