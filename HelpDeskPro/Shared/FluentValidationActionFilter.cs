namespace HelpDeskPro.Shared
{
    using FluentValidation;
    using FluentValidation.Results;
    using HelpDeskPro.Exceptions;
    using HelpDeskPro.Middleware;
    using Microsoft.AspNetCore.Mvc.Filters;

    public class FluentValidationActionFilter(IServiceProvider _serviceProvider) : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(
            ActionExecutingContext context,
            ActionExecutionDelegate next)
        {
            foreach (var arg in context.ActionArguments.Values)
            {
                if (arg is null) continue;

                var validatorType = typeof(IValidator<>).MakeGenericType(arg.GetType());
                var validatorObj = _serviceProvider.GetService(validatorType);
                if (validatorObj is null) continue;

                var validator = (IValidator)validatorObj;
                ValidationResult result = await validator.ValidateAsync(
                    new ValidationContext<object>(arg));

                if (!result.IsValid)
                {
                    // Obtener para cada código de error el mensaje traducido con GetTranslation() y fallback al mensaje original del error y almacenarlo en una lista
                    var errorMessagesArray = result.Errors
                        .Select(e =>
                        {
                            var attempted = e.AttemptedValue?.ToString() ?? string.Empty;
                            return ApiExceptionMiddleware.GetTranslation(
                                e.ErrorCode,
                                e.ErrorMessage,
                                attempted.Length > 0 ? [attempted] : []
                            );
                        })
                        .ToArray();
                    var error = new ValidationDomainException(string.Join(",", errorMessagesArray), [.. result.Errors.Select(e => e.ErrorCode)]);
                    throw error;
                }
            }

            await next();
        }
    }

}
