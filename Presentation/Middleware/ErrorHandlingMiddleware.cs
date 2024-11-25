using System.Net;
using System.Text.Json;
using CleanArchitecture.Application.Common.Exceptions;

namespace Presentation.Middleware
{
    public class ErrorHandlingMiddleware
	{
        private readonly RequestDelegate next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (BusinessRuleException ex)
            {
                await HandleBusinessExceptionAsync(context, ex);
            }
            catch (ValidationException ex)
            {
                await HandleValidationExceptionAsync(context, ex);
            }
        }

        private static Task HandleBusinessExceptionAsync(HttpContext context, BusinessRuleException ex)
        {
            var result = JsonSerializer.Serialize(new { message = ex.Message });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int) HttpStatusCode.BadRequest;
            return context.Response.WriteAsync(result);
        }

        private static Task HandleValidationExceptionAsync(HttpContext context, ValidationException ex)
        {
            var result = JsonSerializer.Serialize(new { message = ex.GetErrorMessagesAsString() });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return context.Response.WriteAsync(result);
        }
    }
}

