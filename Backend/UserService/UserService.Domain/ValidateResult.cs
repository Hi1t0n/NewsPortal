using System.Net;

namespace UserService.Domain;

public class ValidateResult
{
    public bool IsValid { get; }
    public HttpStatusCode StatusCode { get; }
    public string Message { get; }

    private ValidateResult(bool isValid, HttpStatusCode statusCode, string message)
    {
        IsValid = isValid;
        StatusCode = statusCode;
        Message = message;
    }

    public static ValidateResult Valid() =>
        new ValidateResult(true, HttpStatusCode.OK, string.Empty);

    public static ValidateResult Invalid(HttpStatusCode statusCode, string message) =>
        new ValidateResult(false, statusCode, message);


}