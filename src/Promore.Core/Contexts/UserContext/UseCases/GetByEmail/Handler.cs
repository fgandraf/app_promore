using Promore.Core.Contexts.UserContext.Contracts;

namespace Promore.Core.Contexts.UserContext.UseCases.GetByEmail;

public class Handler(IUserRepository userRepository)
{
    public async Task<OperationResult<Response>> Handle(string address)
    {
        var user = await userRepository.GetByEmailAsync(address);
        if (user is null)
            return OperationResult<Response>.FailureResult($"Usuário '{address}' não encontrado!");
        
        return OperationResult<Response>.SuccessResult(user);
    }
}