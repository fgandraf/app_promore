using Microsoft.EntityFrameworkCore;
using Promore.Api.Data;
using Promore.Core.Handlers;
using Promore.Core.Requests.Regions;
using Promore.Core.Responses.Roles;

namespace Promore.Api.Handlers;

public class RoleHandler(PromoreDataContext context) : IRoleHandler
{
    public Task<List<GetRolesResponses>> GetRolesByUserIdListAsync(GetRolesByUserIdListRequest request)
    {
        var roles = context
             .Roles
             .Where(role => request.RolesIds.Contains(role.Id))
             .Select(x=> new GetRolesResponses())
             .ToListAsync();

         return roles;
    }
}