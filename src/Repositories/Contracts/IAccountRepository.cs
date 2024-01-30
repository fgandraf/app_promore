using PromoreApi.ViewModels;

namespace PromoreApi.Repositories.Contracts;

public interface IAccountRepository
{
    Task<bool> LoginAsync(LoginVO model);
}