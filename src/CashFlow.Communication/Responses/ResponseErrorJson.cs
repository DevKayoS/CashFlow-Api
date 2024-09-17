namespace CashFlow.Communication.Responses;

public class ResponseErrorJson
{
    public List<string> ErrorMessages { get; set; } = [];

    public ResponseErrorJson(string errorMessage)
    {
        //[errorMessage] is the same new List<string> { errorMessage}
        ErrorMessages = [errorMessage];
    }

    public ResponseErrorJson(List<string> errorMessages)
    {
        ErrorMessages = errorMessages;
    }
}