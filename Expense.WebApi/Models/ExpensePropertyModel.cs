using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expense.WebApi.Models
{
    /// <summary>
    /// Expense property view model
    /// </summary>
    public class ExpensePropertyModel
    {
        public string Name { get; set; }

        public string Value { get; set; }
    }
}
