using Promore.Core.Contexts.UserContext.Contracts;
using Promore.Core.Contexts.UserContext.Entities;
using SecureIdentity.Password;

namespace Promore.Core.Contexts.UserContext.UseCases.GetByLogin;

public class Handler(IUserRepository userRepository)
{
    public async Task<OperationResult<User>> Handle(LoginRequest model)
    { 
        var user = await userRepository.GetUserByLogin(model); 
        
        if (user is null || !user.Active)
            return OperationResult<User>.FailureResult($"Usuário '{model.Email}' não encontrado ou não está ativo!");
        if (!PasswordHasher.Verify(user!.PasswordHash, model.Password))
            return OperationResult<User>.FailureResult("Usuário ou senha inválida!");
        
        return OperationResult<User>.SuccessResult(user);
    }
}