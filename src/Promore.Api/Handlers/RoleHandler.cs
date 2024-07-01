using Microsoft.EntityFrameworkCore;
using Promore.Api.Data;
using Promore.Core.Handlers;
using Promore.Core.Models;
using Promore.Core.Requests.Regions;
using Promore.Core.Responses;

namespace Promore.Api.Handlers;

public class RoleHandler(PromoreDataContext context) : IRoleHandler
{
    public async Task<Response<List<Role>?>> GetAllByUserIdAsync(GetRolesByUserIdListRequest request)
    {
        try
        {
            var roles = await context
                .Roles
                .Where(role => request.RolesIds.Contains(role.Id))
                .ToListAsync();
            
            return roles.Count == 0 
                ? new Response<List<Role>?>(null, 404, "Nenhum papel cadastrado!") 
                : new Response<List<Role>?>(roles);
        }
        catch
        {
            return new Response<List<Role>?>(null, 500, "[AHROGA12] Não foi possível encontrar os papéis!");
        }
    }
}