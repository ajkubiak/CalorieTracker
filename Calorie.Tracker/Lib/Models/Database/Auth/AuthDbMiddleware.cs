//using System;
//using System.Linq;
//using System.Security.Claims;
//using System.Threading.Tasks;
//using Lib.Utils;
//using Microsoft.AspNetCore.Builder;
//using Microsoft.AspNetCore.Http;

//namespace Lib.Models.Database.Auth
//{
//    public class AuthDbMiddleware
//    {
//        private readonly RequestDelegate _next;

//        public AuthDbMiddleware(RequestDelegate next)
//        {
//            _next = next;
//        }

//        // IMyScopedService is injected into Invoke
//        public async Task Invoke(HttpContext httpContext, IAuthDb svc)
//        {
//            var nameClaim = httpContext.User.Claims.Where(x => x.Type == ClaimTypes.Name).SingleOrDefault();
//            var id = nameClaim.Value;


//            await _next(httpContext);
//        }
//    }

//    public static class AuthDbMiddlewareExtensions
//    {
//        public static IApplicationBuilder UseAuthDbMiddleware(
//            this IApplicationBuilder builder)
//        {
//            return builder.UseMiddleware<AuthDbMiddleware>();
//        }
//    }
//}
