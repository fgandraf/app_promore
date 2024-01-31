using PromoreApi.ViewModels;

namespace PromoreApi.Repositories.Contracts;

public interface IUserRepository
{
    Task<List<UserGetVO>> GetAll();
    Task<UserGetVO> GetByIdAsync(int id);
    Task<UserGetVO> GetByEmailAddress(string address);
    Task<long> InsertAsync(UserCreateVO model);
    Task<bool> UpdateAsync(UserUpdateVO model);
    Task<bool> DeleteAsync(int id);
}