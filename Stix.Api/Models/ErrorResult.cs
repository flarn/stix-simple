namespace Stix.Api.Models
{
    public class ErrorResult
    {
        public ErrorResult(string message) => Message = message;

        public string Message { get; }
    }
}
