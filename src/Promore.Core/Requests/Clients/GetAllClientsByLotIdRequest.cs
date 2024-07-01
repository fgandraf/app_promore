namespace Promore.Core.Requests.Clients;

public class GetAllClientsByLotIdRequest : Request
{
    public int LotId { get; set; }
}