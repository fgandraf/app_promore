namespace Promore.Core.Contracts;

public interface IRoleRepository
{
    Task<List<Entities.Role>> GetRolesByIdListAsync(List<int> rolesId);
}