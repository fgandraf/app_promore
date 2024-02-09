using Promore.Core;
using Promore.Core.Contracts;
using Promore.Core.ViewModels.Responses;

namespace Promore.UseCases.User.UseCases;

public class GetById(IUserRepository userRepository)
{
    public async Task<OperationResult<UserResponse>> Handle(int id)
    {
        var user = await userRepository.GetByIdAsync(id);
        if (user is null)
            return OperationResult<UserResponse>.FailureResult($"Usuário '{id}' não encontrado!");
        
        return OperationResult<UserResponse>.SuccessResult(user);
    }
}