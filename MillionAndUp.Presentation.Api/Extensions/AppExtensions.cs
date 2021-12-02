using Microsoft.AspNetCore.Builder;
using MillionAndUp.Presentation.Api.Middlewares;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MillionAndUp.Presentation.Api.Extensions
{
    public static class AppExtensions
    {   
        public static void UseErrorHandlingMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ErrorHandlerMiddleware>();
        }
    }
}
