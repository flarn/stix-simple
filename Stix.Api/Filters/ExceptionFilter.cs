using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Stix.Api.Models;
using Stix.Core;
using System.Net;

namespace Stix.Api.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        private readonly IHostEnvironment _hostEnvironment;

        public ExceptionFilter(IHostEnvironment hostEnvironment) => _hostEnvironment = hostEnvironment;

        public void OnException(ExceptionContext context)
        {
            if (_hostEnvironment.IsDevelopment())
                return;

            if (context.Exception is EntityNotFoundException entityNotFoundException)
            {
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
                context.Result = new NotFoundResult();
            }
            else
            {
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Result = new ObjectResult(new ErrorResult(context.Exception.Message));
            }
        }
    }
}