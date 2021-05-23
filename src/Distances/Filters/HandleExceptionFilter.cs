using System;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Distances.Filters
{
    public class HandleExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            throw new NotImplementedException();
        }
    }
}