using backend.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace backend.Config{
    public class RestExceptionHandler : IExceptionFilter{
        public void OnException(ExceptionContext context){
            var exception = context.Exception;
            var statusCode = HttpStatusCode.InternalServerError;
            var message = "Ocurrió un error inesperado.";

            // Manejo de excepciones específicas
            if (exception is ResourceNotFoundException) {
                statusCode = HttpStatusCode.NotFound;
                message = exception.Message;
            }
            else if (exception is ResourceDuplicateException){
                statusCode = HttpStatusCode.Conflict;
                message = exception.Message;
            }
            else if (exception is BadRequestException){
                statusCode = HttpStatusCode.BadRequest;
                message = exception.Message;
            }

            var problemDetails = new ProblemDetails{
                Status = (int)statusCode,
                Title = message,
                Detail = exception.Message
            };

            context.Result = new ObjectResult(problemDetails){
                StatusCode = (int)statusCode
            };

            context.ExceptionHandled = true;
        }
    }
}