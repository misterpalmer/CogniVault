namespace CogniVault.Platform.Core.RestApi.Responses;

public class ApiResponse<T> : ApiResponse
{
    public T Data { get; set; }
}

public class ApiResponse
{
    public ApiResponse()
    {
        Outcome = new OperationOutcome
        {
            Errors = Enumerable.Empty<string>(),
            Message = string.Empty,
            OpResult = OpResult.Success // optimistic 🤞
        };
    }

    public OperationOutcome Outcome { get; set; }
}