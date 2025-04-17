using System.ComponentModel.DataAnnotations;

namespace TF1.LeaveManagement.API.Middleware
{
    public static class ErrorHandlerMiddlewareExtensions
    {
        public static IApplicationBuilder UseGlobalExceptionHandler(this IApplicationBuilder app)
        {
            return app.UseExceptionHandler(errorApp =>
            {
                errorApp.Run(async context =>
                {
                    context.Response.ContentType = "application/json";

                    var exceptionHandlerPathFeature = context.Features.Get<Microsoft.AspNetCore.Diagnostics.IExceptionHandlerPathFeature>();
                    var exception = exceptionHandlerPathFeature?.Error;

                    var response = exception is ValidationException
                        ? new { Message = "Validation failed", Error = exception.Message }
                        : new { Message = "An unexpected error occurred.", Error = exception?.Message ?? "" };

                    context.Response.StatusCode = exception is ValidationException ? 400 : 500;

                    await context.Response.WriteAsJsonAsync(response);
                });
            });
        }
    }
}
