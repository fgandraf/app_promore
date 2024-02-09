using Promore.Core;
using Promore.Core.Contracts;
using Promore.Core.ViewModels.Responses;

namespace Promore.UseCases.User.UseCases;

public class GetByEmail(IUserRepository userRepository)
{
    public async Task<OperationResult<UserResponse>> Handle(string address)
    {
        var user = await userRepository.GetByEmailAsync(address);
        if (user is null)
            return OperationResult<UserResponse>.FailureResult($"Usuário '{address}' não encontrado!");
        
        return OperationResult<UserResponse>.SuccessResult(user);
    }
}