using Promore.Core.Models;
using Promore.Core.Requests.Regions;
using Promore.Core.Responses;

namespace Promore.Core.Handlers;

public interface IRoleHandler
{
    Task<Response<List<Role>?>> GetAllByUserIdAsync(GetRolesByUserIdListRequest request);
}