using Promore.Core.Entities;
using Promore.Core.Models.InputModels;

namespace Promore.Core.Contracts;

public interface IAccountRepository
{
    Task<User> LoginAsync(LoginInput model);
}