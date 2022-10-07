using Newtonsoft.Json;
using System.Net;

namespace DK_Project.Middleware
{
    public class CustomErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<CustomErrorHandlerMiddleware> _logger;
        public CustomErrorHandlerMiddleware(RequestDelegate next, ILogger<CustomErrorHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Method == "GET")
            {
                for (int i = 0; i < 15; i++)
                {
                    _logger.LogInformation("THIS IS MIDDLEWARE");
                }
            }
            await _next(context);
        }
    }
}
