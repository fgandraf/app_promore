using Promore.Core.Entities;
using Promore.Core.Models.InputModels;

namespace Promore.Core.Repositories.Contracts;

public interface IAccountRepository
{
    Task<User> LoginAsync(LoginInput model);
}