namespace Promore.Core.Requests.Users;

public class GetUserByEmailRequest : Request
{
    public string Email { get; set; } = string.Empty;
}