namespace Promore.Core.Contexts.RoleContext.Contracts;

public interface IRoleRepository
{
    Task<List<Entities.Role>> GetRolesByIdListAsync(List<int> rolesId);
}