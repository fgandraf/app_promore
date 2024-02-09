using Promore.Core.Contexts.UserContext.Contracts;

namespace Promore.Core.Contexts.UserContext.UseCases.GetById;

public class Handler(IUserRepository userRepository)
{
    public async Task<OperationResult<Response>> Handle(int id)
    {
        var user = await userRepository.GetByIdAsync(id);
        if (user is null)
            return OperationResult<Response>.FailureResult($"Usuário '{id}' não encontrado!");
        
        return OperationResult<Response>.SuccessResult(user);
    }
}