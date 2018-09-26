using Expense.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expense.Service
{
    /// <summary>
    /// Sub class of domain model ExpenseProperty, it contains state to indicate a property matching status.
    /// </summary>
    internal class StatedExpenseProperty : ExpenseProperty
    {
        public PropertyMatchingState State { get; set; } = PropertyMatchingState.NotAssigned; 
    }
}
