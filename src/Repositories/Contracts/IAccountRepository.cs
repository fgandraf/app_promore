using PromoreApi.Entities;
using PromoreApi.Models.InputModels;

namespace PromoreApi.Repositories.Contracts;

public interface IAccountRepository
{
    Task<User> LoginAsync(LoginInput model);
}