using backend.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace backend.Config
{
    public class RestExceptionHandler : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            // Si el error proviene de una validación del modelo
            if (context.Exception is ValidationException)
            {
                // Aquí manejas la validación y creas una respuesta personalizada
                var response = new
                {
                    status = (int)HttpStatusCode.BadRequest,
                    title = "Errores de validación",
                    detail = "Los datos enviados no son válidos.",
                    errors = context.ModelState.ToDictionary(
                        kvp => kvp.Key,
                        kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                    )
                };

                context.Result = new JsonResult(response)
                {
                    StatusCode = (int)HttpStatusCode.BadRequest
                };
                context.ExceptionHandled = true; // Evita que ASP.NET maneje el error por defecto
            }
            else
            {
                // Manejo de otros tipos de excepciones
                var exception = context.Exception;
                var statusCode = HttpStatusCode.InternalServerError;
                var title = "Ocurrió un error inesperado.";
                var detail = exception.Message;

                // Manejo de excepciones específicas
                if (exception is ResourceNotFoundException)
                {
                    statusCode = HttpStatusCode.NotFound;
                    title = "Recurso no encontrado.";
                }
                else if (exception is ResourceDuplicateException)
                {
                    statusCode = HttpStatusCode.Conflict;
                    title = "Conflicto de recurso.";
                }
                else if (exception is BadRequestException)
                {
                    statusCode = HttpStatusCode.BadRequest;
                    title = "Solicitud incorrecta.";
                }

                // Crear una respuesta personalizada sin "traceId" y "type"
                var response = new
                {
                    status = (int)statusCode,
                    title,
                    detail,
                    instance = context.HttpContext.Request.Path // Ruta del recurso que causó el error
                };

                context.Result = new JsonResult(response)
                {
                    StatusCode = (int)statusCode
                };

                context.ExceptionHandled = true; // Evita que ASP.NET maneje el error por defecto
            }
        }
    }
}
