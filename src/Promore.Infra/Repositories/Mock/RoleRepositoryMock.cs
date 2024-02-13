using Promore.Core.Contexts.RoleContext.Contracts;
using Promore.Core.Contexts.RoleContext.Entities;

namespace Promore.Infra.Repositories.Mock;

public class RoleRepositoryMock : IRoleRepository
{
    private MockContext _context;

    public RoleRepositoryMock(MockContext context)
        => _context = context;
    
    public Task<List<Role>> GetRolesByIdListAsync(List<int> rolesId)
    {
        var roles = _context.Roles
            .Where(role => rolesId.Contains(role.Id))
            .ToList();
        
        return Task.FromResult(roles);
    }
}