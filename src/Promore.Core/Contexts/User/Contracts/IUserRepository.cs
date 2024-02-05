using Promore.Core.Contexts.User.Models.Requests;
using Promore.Core.Contexts.User.Models.Responses;

namespace Promore.Core.Contexts.User.Contracts;

public interface IUserRepository
{
    Task<List<ReadUser>> GetAll();
    Task<Entity.User> GetUserByIdAsync(int id);
    Task<ReadUser> GetByIdAsync(int id);
    Task<ReadUser> GetByEmailAddress(string address);
    Task<long> InsertAsync(Entity.User user);
    Task<int> UpdateInfoAsync(Entity.User user);
    Task<int> UpdateSettingsAsync(Entity.User user);
    Task<int> DeleteAsync(int id);
    Task<Entity.User> LoginAsync(Login model);
}