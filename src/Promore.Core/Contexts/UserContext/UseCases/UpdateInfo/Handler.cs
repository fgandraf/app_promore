using Promore.Core.Contexts.UserContext.Contracts;
using SecureIdentity.Password;

namespace Promore.Core.Contexts.UserContext.UseCases.UpdateInfo;

public class Handler(IUserRepository userRepository)
{
    public async Task<OperationResult> Handle(UpdateUserInfoRequest model)
    {
        var user = await userRepository.GetUserByIdAsync(model.Id);
        
        if (user is null || !user.Active)
            return OperationResult.FailureResult($"Usuário '{model.Email}' não encontrado ou não está ativo!");
        
        user.Email = model.Email;
        user.PasswordHash = PasswordHasher.Hash(model.Password);
        user.Name = model.Name;
        user.Cpf = model.Cpf;
        user.Profession = model.Profession;

        var rowsAffected = await userRepository.UpdateAsync(user);

        return rowsAffected > 0 ? OperationResult.SuccessResult() : OperationResult.FailureResult("Não foi possível alterar o usuário!");
    }
}