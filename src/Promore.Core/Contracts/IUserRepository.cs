using Promore.Core.Models.InputModels;
using Promore.Core.Models.ViewModels;

namespace Promore.Core.Repositories.Contracts;

public interface IUserRepository
{
    Task<List<UserView>> GetAll();
    Task<UserView> GetByIdAsync(int id);
    Task<UserView> GetByEmailAddress(string address);
    Task<long> InsertAsync(CreateUserInput model);
    Task<bool> UpdateInfoAsync(UpdateUserInfoInput model);
    Task<bool> UpdateSettingsAsync(UpdateUserSettingsInput model);
    Task<bool> DeleteAsync(int id);
}