using PromoreApi.Models.InputModels;
using PromoreApi.Models.ViewModels;

namespace PromoreApi.Repositories.Contracts;

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