namespace Tasks.Communication.Response;
public class ErrorReponseJson
{
    public List<string> Messages { get; set; }

    public ErrorReponseJson(string menssage)
    {
        Messages = new List<string>
        {
            menssage
        };
    }

    public ErrorReponseJson(List<string> messages)
    {
        Messages = messages;
    }
}
