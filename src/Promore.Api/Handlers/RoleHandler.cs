using Microsoft.EntityFrameworkCore;
using Promore.Api.Data;
using Promore.Core.Handlers;
using Promore.Core.Models;
using Promore.Core.Requests.Regions;
using Promore.Core.Responses;

namespace Promore.Api.Handlers;

public class RoleHandler(PromoreDataContext context) : IRoleHandler
{
    public async Task<Response<List<Role>>> GetRolesByUserIdListAsync(GetRolesByUserIdListRequest request)
    {
        var roles = await context
             .Roles
             .Where(role => request.RolesIds.Contains(role.Id))
             .ToListAsync();
        
        return new Response<List<Role>>(roles);
    }
}