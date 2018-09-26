using Expense.Model;
using Expense.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expense.Test.MockObject
{
    public class MockExpenseService : IExpenseService
    {
        public ExpenseInfo ExtractExpenseInfoFromText(string text)
        {
            var expenseInfo = new ExpenseInfo();

            expenseInfo.AddProperty(new ExpenseProperty()
            {
                Name = "Mock Name",
                Value = "Mock Value"
            }); 

            return expenseInfo; 
        }
    }
}
