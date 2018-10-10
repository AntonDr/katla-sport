using KatlaSport.Services;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;
using NLog;

namespace KatlaSport.WebApi.CustomFilters
{
    public class CustomExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            var ex = context.Exception;

            
            var logger = LogManager.GetCurrentClassLogger();

            if (ex is RequestedResourceNotFoundException)
            {
                logger.Error(ex, "Resource not found");

                context.Response = new HttpResponseMessage(HttpStatusCode.NotFound);
            }
            else if (ex is RequestedResourceHasConflictException)
            {
                logger.Error(ex, "Resource has conflict");

                context.Response = new HttpResponseMessage(HttpStatusCode.Conflict);
            }
            else if (ex is Exception)
            {
                logger.Error(ex,"Other exection");

                context.Response = new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
        }
    }
}
