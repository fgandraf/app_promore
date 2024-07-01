namespace Promore.Core.Requests.Users;

public class RemoveLotFromUserRequest : Request
{
    public int Id { get; set; }
    public int LotId { get; set; }
}