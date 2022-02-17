using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace Middleware.Middlewares
{
    public class CustomExceptionMiddleware
    {
        public readonly RequestDelegate _next;
        public CustomExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            string message = " [Request] HTTP :" + context.Request.Method + " - Path : " + context.Request.Path;
            System.Console.WriteLine(message);
            await _next(context);
        } 


    }
    public static class CustomeExceptionMiddlewareExtension
    {
        public static IApplicationBuilder UseCustomExceptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomExceptionMiddleware>();    
        }
    }
}