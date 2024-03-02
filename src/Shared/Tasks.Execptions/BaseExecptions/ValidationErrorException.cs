namespace Tasks.Execptions.BaseExecptions;
public class ValidationErrorException : TaskExecptions
{
    public List<string> ErrorMessages { get; set; }

    public ValidationErrorException(List<string> errorMessages) : base(string.Empty)
    {
        ErrorMessages = errorMessages;
    }
}
