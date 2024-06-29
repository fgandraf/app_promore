using Promore.Core.Models;
using Promore.Core.Requests.Users;
using Promore.Core.Responses.Users;

namespace Promore.Core.Handlers;

public interface IUserHandler
{
    Task<User> GetUserByLogin(GetUserByLoginRequest model);
    Task<List<GetUsersResponse>> GetAllAsync();
    Task<long> InsertAsync(User user);
    Task<GetUserByIdResponse> GetByIdAsync(int id);
    Task<GetUserByEmailResponse> GetByEmailAsync(string address);
    Task<int> UpdateAsync(User user);

    
    Task<User> GetUserByIdAsync(int id);
    Task<List<User>> GetEntitiesByIdsAsync(List<int> ids);
}