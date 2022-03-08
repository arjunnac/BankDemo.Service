using Microsoft.AspNetCore.Http;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace BankDemo.AccountService
{
    internal class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _reqDelegate;

        public ErrorHandlerMiddleware(RequestDelegate reqDelegate)
        {
            _reqDelegate = reqDelegate;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _reqDelegate(context);
            }
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";

                var result = JsonSerializer.Serialize(new { message = error?.Message, stackTrace = error?.StackTrace });
                await response.WriteAsync(result);
            }
        }
    }
}