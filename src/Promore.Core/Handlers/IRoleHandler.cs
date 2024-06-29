using Promore.Core.Models;

namespace Promore.Core.Handlers;

public interface IRoleHandler
{
    Task<List<Role>> GetRolesByIdListAsync(List<int> rolesId);
}