using Promore.Core.Contexts.UserContext.Contracts;

namespace Promore.Core.Contexts.UserContext.UseCases.GetAll;

public class Handler(IUserRepository userRepository)
{
    public async Task<OperationResult<List<Response>>> Handle()
    {
        var users = await userRepository.GetAllAsync();
        if (users.Count == 0)
            return OperationResult<List<Response>>.FailureResult("Nenhum usuário cadastrado!");
        
        return OperationResult<List<Response>>.SuccessResult(users);
    }
}