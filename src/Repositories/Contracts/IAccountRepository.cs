using PromoreApi.Models.InputModels;

namespace PromoreApi.Repositories.Contracts;

public interface IAccountRepository
{
    Task<bool> LoginAsync(LoginInput model);
}