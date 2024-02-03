using Promore.Api.Entities;
using Promore.Api.Models.InputModels;

namespace Promore.Api.Repositories.Contracts;

public interface IAccountRepository
{
    Task<User> LoginAsync(LoginInput model);
}