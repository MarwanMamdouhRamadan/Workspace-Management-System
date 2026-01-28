using System;
using System.Net;
using System.Text.Json;
using Workspace.Application.Utilities;

namespace Workspace_Management_System.Middlewares
{
    public class ExceptionMiddleware
    {
        RequestDelegate _next;
        ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch(Exception ex) 
            {
                _logger.LogError(ex,ex.Message);
                await HandleExceptionAsync(context,ex);
            }
        }
        private static Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";
            var statusCode = (int)HttpStatusCode.InternalServerError;
            var message = "Something went wrong! Please try again later.";
            switch(ex)
            {
                case UnauthorizedAccessException:
                    statusCode = 401;
                    message = "You are not authorized to access this resource.";
                    break;
                case KeyNotFoundException:
                    statusCode = 404;
                    message = ex.Message;
                    break;
                case ArgumentException:
                    statusCode = 400;
                    message = ex.Message;
                    break;

            }
            context.Response.StatusCode = statusCode;
            var response = ApiResponseHelper.FailureObject
                (
                 Errors :message,
                 StatusCode:statusCode
                );
            var json = JsonSerializer.Serialize(response);
            return context.Response.WriteAsync(json);
        }
    }
}
