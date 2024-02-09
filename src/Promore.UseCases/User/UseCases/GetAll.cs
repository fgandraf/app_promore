using Promore.Core;
using Promore.Core.Contracts;
using Promore.Core.ViewModels.Responses;

namespace Promore.UseCases.User.UseCases;

public class GetAll(IUserRepository userRepository)
{
    public async Task<OperationResult<List<UserResponse>>> Handle()
    {
        var users = await userRepository.GetAllAsync();
        if (users.Count == 0)
            return OperationResult<List<UserResponse>>.FailureResult("Nenhum usuário cadastrado!");
        
        return OperationResult<List<UserResponse>>.SuccessResult(users);
    }
}