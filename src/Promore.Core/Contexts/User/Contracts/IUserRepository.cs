using Promore.Core.Contexts.User.Models.Requests;
using Promore.Core.Contexts.User.Models.Responses;

namespace Promore.Core.Contexts.User.Contracts;

public interface IUserRepository
{
    Task<Entity.User> GetUserByLogin(Login model);
    Task<List<ReadUser>> GetAllAsync();
    Task<long> InsertAsync(Entity.User user);
    Task<ReadUser> GetByIdAsync(int id);
    Task<int> UpdateAsync(Entity.User user);

    
    Task<Entity.User> GetEntityByIdAsync(int id);
    Task<List<Entity.User>> GetEntitiesByIdsAsync(List<int> ids);
}