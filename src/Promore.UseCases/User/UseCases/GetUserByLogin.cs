using Promore.Core;
using Promore.Core.Contracts;
using Promore.Core.ViewModels.Requests;
using SecureIdentity.Password;

namespace Promore.UseCases.User.UseCases;

public class GetUserByLogin(IUserRepository userRepository)
{
    public async Task<OperationResult<Core.Entities.User>> Handle(LoginRequest model)
    { 
        var user = await userRepository.GetUserByLogin(model); 
        
        if (user is null || !user.Active)
            return OperationResult<Core.Entities.User>.FailureResult($"Usuário '{model.Email}' não encontrado ou não está ativo!");
        if (!PasswordHasher.Verify(user!.PasswordHash, model.Password))
            return OperationResult<Core.Entities.User>.FailureResult("Usuário ou senha inválida!");
        
        return OperationResult<Core.Entities.User>.SuccessResult(user);
    }
}