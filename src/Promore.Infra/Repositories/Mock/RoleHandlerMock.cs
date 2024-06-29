using Promore.Core.Handlers;
using Promore.Core.Models;

namespace Promore.Infra.Repositories.Mock;

public class RoleHandlerMock : IRoleHandler
{
    private MockContext _context;

    public RoleHandlerMock(MockContext context)
        => _context = context;
    
    public Task<List<Role>> GetRolesByIdListAsync(List<int> rolesId)
    {
        var roles = _context.Roles
            .Where(role => rolesId.Contains(role.Id))
            .ToList();
        
        return Task.FromResult(roles);
    }
}