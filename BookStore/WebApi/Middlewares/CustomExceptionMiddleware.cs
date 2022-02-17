using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace WebApi.Middleware
{
    public class CustomExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        public CustomExceptionMiddleware(RequestDelegate next)
        {
            _next = next;

        }
        public async Task Invoke(HttpContext context)
        {
            var time = Stopwatch.StartNew();
            try
            {
                string message = "[Request] HTTP:" + context.Request.Method + " \n[Path]: " + context.Request.Path + " [Responded]: " + context.Response.StatusCode + " in " + time.ElapsedMilliseconds + " ms.";
                System.Console.WriteLine(message);
                await _next(context);
                time.Stop();

            }
            catch (Exception ex)
            {
                time.Stop();
                await HandleExceptionAsync(context, ex, time);
            }
            
           
            
          
        }

        private Task HandleExceptionAsync(HttpContext context, Exception ex, Stopwatch time)
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";

            string message = 
                "[ERROR] :"+ ex.Message +
                "[Request] HTTP :" + context.Request.Method +
                "[Path] :" + context.Request.Path+
                "[Responded]" + context.Response.StatusCode +" in "+ time.ElapsedMilliseconds + " ms.";

            Console.WriteLine(message);

            var result = JsonConvert.SerializeObject(new
            {
                error = ex.Message
            },Formatting.None);

            return context.Response.WriteAsync(result);
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
