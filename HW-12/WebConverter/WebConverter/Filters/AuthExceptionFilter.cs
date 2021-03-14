using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebConverter.Models;

namespace WebConverter.Filters
{
    /// <summary>
    /// Exception Filter attribute
    /// </summary>
    public class AuthExceptionFilter : ExceptionFilterAttribute
    {
        /// <summary>
        /// Method that handle an exception.
        /// </summary>
        /// <param name="context">Exception Context.</param>
        public override void OnException(ExceptionContext context)
        {
            if (!context.ExceptionHandled)
            {
                int codeException;
                switch (context.Exception)
                {
                    case NullReferenceException:
                        codeException = 11;
                        break;
                    case NotImplementedException:
                        codeException = 12;
                        break;
                    case ArgumentException:
                        codeException = 20;
                        break;
                    case IndexOutOfRangeException:
                        codeException = 30;
                        break;
                    case FileNotFoundException:
                        codeException = 40;
                        break;
                    default: codeException = 1;
                        break;
                }

                ExceptionInfo exeption = new ExceptionInfo()
                {
                    Code = codeException,
                    Message = context.Exception.Message
                };

                JsonResult jsonResult = new JsonResult(exeption);

                context.Result = jsonResult;
                context.ExceptionHandled = true;
            }
        }
    }
}
