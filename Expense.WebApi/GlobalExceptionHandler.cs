using Expense.Model.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Results;

namespace Expense.WebApi
{
    public class GlobalExceptionHandler : ExceptionHandler
    {
        public override void Handle(ExceptionHandlerContext context)
        {
            if (context.Exception is MissingTotalPropertyException)
            {
                var responseMessage = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    ReasonPhrase = "Missing <total> element."
                };

                context.Result = new ResponseMessageResult(responseMessage); 
            }
            else if (context.Exception is XmlTagNotMatchingException)
            {
                var responseMessage = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    ReasonPhrase = "Opening tags that have no corresponding closing tag."
                };

                context.Result = new ResponseMessageResult(responseMessage);
            }
            else
            {
                var responseMessage = new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    ReasonPhrase = context.Exception.Message
                };

                context.Result = new ResponseMessageResult(responseMessage);
            }

            base.Handle(context);
        }
    }
}
