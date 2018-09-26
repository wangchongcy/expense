using Expense.Infrastructure;
using Expense.Model;
using Expense.Service;
using Expense.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Expense.WebApi.Controllers
{
    /// <summary>
    /// A controller used to extract expense information from a block of text.
    /// </summary>
    public class TextExtractController : ApiController
    {
        private IExpenseService expenseService;
        private ILogger logger; 

        public TextExtractController(ILogger logger, IExpenseService expenseService)
        {
            this.logger = logger; 
            this.expenseService = expenseService; 
        }

        /// <summary>
        /// Handles http post reqest
        /// </summary>
        /// <param name="text">request body</param>
        /// <returns>Expense info</returns>
        public ExpenseInfoModel Post([FromBody]string text)
        {
            logger.Debug($"{nameof(TextExtractController)}-{nameof(Post)} request: {text}"); 
            var expenseInfo = expenseService.ExtractExpenseInfoFromText(text);
            return new ExpenseInfoModel(expenseInfo);
        }
    }
}
