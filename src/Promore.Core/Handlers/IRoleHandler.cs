using Promore.Core.Requests.Regions;
using Promore.Core.Responses.Roles;

namespace Promore.Core.Handlers;

public interface IRoleHandler
{
    Task<List<GetRolesResponses>> GetRolesByUserIdListAsync(GetRolesByUserIdListRequest request);
}