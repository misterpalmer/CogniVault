namespace CogniVault.Platform.Core.RestApi.Responses;

public class OperationOutcome
{
    public OperationOutcome()
    {
        Message = string.Empty;
        ErrorId = string.Empty;
    }

    public OpResult OpResult { get; set; }
    public string ErrorId { get; set; }
    public string Message { get; set; }
    public FailType FailType { get; set; }

    public dynamic Errors { get; set; }

    public static OperationOutcome SuccessfulOutcome => new()
    {
        Errors = Enumerable.Empty<string>(),
        ErrorId = string.Empty,
        FailType = FailType.None,
        Message = string.Empty,
        OpResult = OpResult.Success
    };

    public static OperationOutcome UnSuccessfulOutcome => new()
    {
        Errors = Enumerable.Empty<string>(),
        ErrorId = string.Empty,
        FailType = FailType.Error,
        Message = string.Empty,
        OpResult = OpResult.Fail
    };

    public static OperationOutcome ValidationFailOutcome(dynamic errors, string message = null)
    {
        return new OperationOutcome
        {
            Errors = errors,
            ErrorId = string.Empty,
            FailType = FailType.ValidationFailure,
            Message = message ?? string.Empty,
            OpResult = OpResult.Fail
        };
    }
}