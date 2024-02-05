namespace Promore.Core.Contexts.Role.Contracts;

public interface IRoleRepository
{
    Task<List<Entity.Role>> GetRolesByIdListAsync(List<int> rolesId);
}