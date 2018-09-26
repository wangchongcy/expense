﻿using Expense.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expense.Service
{
    public interface IExpenseService
    {
        ExpenseInfo ExtractExpenseInfoFromText(string text); 
    }
}
