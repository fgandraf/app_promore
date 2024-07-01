namespace Promore.Core.Requests.Regions;

public class GetRolesByUserIdListRequest : Request
{
    public List<int> RolesIds { get; set; }
}