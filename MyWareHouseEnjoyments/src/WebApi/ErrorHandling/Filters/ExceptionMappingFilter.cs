﻿using Microsoft.AspNetCore.Mvc.Filters;
using MyWarehouse.Application.Common.Exceptions;
using System.Diagnostics.CodeAnalysis;

namespace MyWarehouse.WebApi.ErrorHandling;

/// <summary>
/// Maps exceptions occurred in lower layers into HTTP responses with appropriate HTTP code.
/// Make sure to register this filter globally.
/// </summary>
public class ExceptionMappingFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    { 
        context.Result = GetExceptionResult(context.Exception);
        context.ExceptionHandled = true;
    }

    [ExcludeFromCodeCoverage] // Seems like coverlet doesn't register switch expressions properly; it is tested.
    private static IActionResult GetExceptionResult(Exception exception)
        => exception switch
        {
            InputValidationException e => HandleValidationException(e),
            EntityNotFoundException e => HandleNotFoundException(e),
            _ => HandleUnknownException(exception)
        };

    private static IActionResult HandleUnknownException(Exception _)
    {
        var details = new ProblemDetails
        {
            Status = StatusCodes.Status500InternalServerError,
            Title = "An error occurred while processing your request.",
            Type = "https://tools.ietf.org/html/rfc7231#section-6.6.1"
        };

        return new ObjectResult(details) { StatusCode = StatusCodes.Status500InternalServerError };
    }

    private static IActionResult HandleValidationException(InputValidationException exception)
    {
        var details = new ValidationProblemDetails(exception.Errors)
        {
            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1"
        };

        return new BadRequestObjectResult(details);
    }

    private static IActionResult HandleNotFoundException(EntityNotFoundException exception)
    {
        var details = new ProblemDetails()
        {
            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.4",
            Title = "The specified resource was not found.",
            Detail = exception.Message
        };

        return new NotFoundObjectResult(details);
    }
}
