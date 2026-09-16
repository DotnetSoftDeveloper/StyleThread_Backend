using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.Middlewares
{
    public class CustomAuthorizationMiddleware(RequestDelegate _next)
    {
        public async Task Invoke(HttpContext context)
        {
            await _next(context);

            if (context.Response.StatusCode == 401) // Unauthorized
            {
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(JsonConvert.SerializeObject(new
                {
                    Success = false,
                    Message = "You are not authorized to access this resource."
                }));
            }
        }
    }
}
