using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.WebApi.Common;
using FluentValidation;
using System.Text.Json;

namespace Ambev.DeveloperEvaluation.WebApi.Middleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (ValidationException ex)
        {
            await HandleValidationExceptionAsync(context, ex);
        }
        catch (KeyNotFoundException ex)
        {
            await HandleKeyNotFoundExceptionAsync(context, ex);
        }
        catch (UnauthorizedAccessException ex)
        {
            await HandleUnauthorizedAccessAsync(context, ex);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        => HandlebaseAsync(context, StatusCodes.Status500InternalServerError, exception.Message);

    private static Task HandleValidationExceptionAsync(HttpContext context, ValidationException exception)
        => HandlebaseAsync(context, StatusCodes.Status400BadRequest, "Validation Failed", exception.Errors.Select(error => (ValidationErrorDetail)error));

    private static Task HandleUnauthorizedAccessAsync(HttpContext context, Exception exception)
        => HandlebaseAsync(context, StatusCodes.Status401Unauthorized, exception.Message);

    private static Task HandleKeyNotFoundExceptionAsync(HttpContext context, KeyNotFoundException exception)
        => HandlebaseAsync(context, StatusCodes.Status404NotFound, exception.Message);

    private static Task HandlebaseAsync(HttpContext context, int statusCodes, string message)
        => HandlebaseAsync(context, statusCodes, message, []);

    private static Task HandlebaseAsync(HttpContext context, int statusCodes, string message, IEnumerable<ValidationErrorDetail> errors)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = statusCodes;

        var response = new ApiResponse
        {
            Success = false,
            Message = message
        };

        if(errors != null && errors.Any())
            response.Errors = errors;

        var jsonOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        return context.Response.WriteAsync(JsonSerializer.Serialize(response, jsonOptions));
    }
}
