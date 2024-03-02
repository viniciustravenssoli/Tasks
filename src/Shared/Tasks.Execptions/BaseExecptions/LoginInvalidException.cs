namespace Tasks.Execptions.BaseExecptions;
public class LoginInvalidException : TaskExecptions
{
    public LoginInvalidException() : base(ResourceErrorsMessage.InvalidLogin)
    {

    }
}
