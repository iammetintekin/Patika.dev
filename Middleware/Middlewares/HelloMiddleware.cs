using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Builder;

namespace Middleware.Middlewares
{
    public class HelloMiddleware
    {
        //request delegate alıyor
        private readonly RequestDelegate _next;
        public HelloMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            System.Console.WriteLine("Invoke hello world");
            await _next.Invoke(context);
            System.Console.WriteLine("Invoke bye world");
        }
    }

    static public class MiddlewareExtension{
        public static IApplicationBuilder UseHello(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<HelloMiddleware>();
        }
    }
}