using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Threading.Tasks;



namespace Api.Utils
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class ElogMiddleware
    {
        private readonly RequestDelegate _next;



        public ElogMiddleware(RequestDelegate next)
        {
            _next = next;
        }



        public async Task Invoke(HttpContext httpContext)
        {
            if (httpContext.Response.StatusCode != 200)
            {
                httpContext.Response.ContentLength = 0;
                //Here to wite Error logging process
            }
            await _next(httpContext);
        }
    }



    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class ElogMiddlewareExtensions
    {
        public static IApplicationBuilder UseElog(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ElogMiddleware>();
        }
    }
}