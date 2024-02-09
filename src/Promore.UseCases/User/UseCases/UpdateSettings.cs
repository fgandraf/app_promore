using Promore.Core;
using Promore.Core.Contracts;
using Promore.Core.ViewModels.Requests;

namespace Promore.UseCases.User.UseCases;

public class UpdateSettings(
    IUserRepository userRepository,
    IRegionRepository regionRepository,
    IRoleRepository roleRepository)
{
    public async Task<OperationResult> Handle(UserUpdateSettingsRequest model)
    {
        var user = await userRepository.GetEntityByIdAsync(model.Id);
        
        if (user is null || !user.Active)
            return OperationResult.FailureResult($"Usuário '{model.Id}' não encontrado ou não está ativo!");
        
        var regions = regionRepository.GetRegionsByIdListAsync(model.Regions).Result;
        var roles = roleRepository.GetRolesByIdListAsync(model.Roles).Result;

        user.Active = model.Active;
        user.Roles = roles;
        user.Regions = regions;

        var rowsAffected = await userRepository.UpdateAsync(user);

        return rowsAffected > 0 ? OperationResult.SuccessResult() : OperationResult.FailureResult("Não foi possível alterar o usuário!");
    }
}