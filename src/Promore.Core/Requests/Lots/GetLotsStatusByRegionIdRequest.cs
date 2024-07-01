namespace Promore.Core.Requests.Lots;

public class GetLotsStatusByRegionIdRequest : PagedRequest
{
    public int RegionId { get; set; }
}