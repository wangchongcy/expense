using Expense.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expense.WebApi.Models
{
    /// <summary>
    /// Expense info view model
    /// </summary>
    public class ExpenseInfoModel
    {
        public ExpenseInfoModel(ExpenseInfo domainModel)
        {
            ExpenseProperties = (from property in domainModel.Properties
                          select new ExpensePropertyModel()
                          {
                              Name = property.Name,
                              Value = property.Value
                          }).ToList();
        }

        public List<ExpensePropertyModel> ExpenseProperties { get; }
    }
}
