using Promore.Core.Models;
using Promore.Core.Requests.Regions;
using Promore.Core.Responses;
using Promore.Core.Responses.Roles;

namespace Promore.Core.Handlers;

public interface IRoleHandler
{
    Task<Response<List<Role>>> GetRolesByUserIdListAsync(GetRolesByUserIdListRequest request);
}