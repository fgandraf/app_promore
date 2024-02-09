using Promore.Core.ViewModels.Requests;
using Promore.Core.ViewModels.Responses;

namespace Promore.Core.Contracts;

public interface IUserRepository
{
    Task<Entities.User> GetUserByLogin(LoginRequest model);
    Task<List<UserResponse>> GetAllAsync();
    Task<long> InsertAsync(Entities.User user);
    Task<UserResponse> GetByIdAsync(int id);
    Task<UserResponse> GetByEmailAsync(string address);
    Task<int> UpdateAsync(Entities.User user);

    
    Task<Entities.User> GetEntityByIdAsync(int id);
    Task<List<Entities.User>> GetEntitiesByIdsAsync(List<int> ids);
}