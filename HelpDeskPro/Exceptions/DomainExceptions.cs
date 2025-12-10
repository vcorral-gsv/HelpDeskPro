namespace HelpDeskPro.Exceptions
{
    public interface IWithErrorCodes
    {
        string[] ErrorCodes { get; }
    }
    public abstract class DomainException(string message) : Exception(message)
    {
    }
    public class NotFoundException(string message) : KeyNotFoundException(message)
    {
    }

    public class UnauthorizedDomainException(string message) : UnauthorizedAccessException(message)
    {
    }

    public class ForbiddenException(string message) : DomainException(message)
    {
    }

    public class ValidationDomainException(string message, string[] errorCodes) : DomainException(message), IWithErrorCodes
    {
        public string[] ErrorCodes { get; } = errorCodes;
    }

    public class ConflictException(string message) : DomainException(message)
    {
    }
}
