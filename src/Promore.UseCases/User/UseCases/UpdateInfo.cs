using Promore.Core;
using Promore.Core.Contracts;
using Promore.Core.ViewModels.Requests;
using SecureIdentity.Password;

namespace Promore.UseCases.User.UseCases;

public class UpdateInfo(IUserRepository userRepository)
{
    public async Task<OperationResult> Handle(UserUpdateInfoRequest model)
    {
        var user = await userRepository.GetEntityByIdAsync(model.Id);
        
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