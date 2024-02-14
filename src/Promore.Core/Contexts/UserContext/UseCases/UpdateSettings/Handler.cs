using Promore.Core.Contexts.RegionContext.Contracts;
using Promore.Core.Contexts.RoleContext.Contracts;
using Promore.Core.Contexts.UserContext.Contracts;

namespace Promore.Core.Contexts.UserContext.UseCases.UpdateSettings;

public class Handler(
    IUserRepository userRepository,
    IRegionRepository regionRepository,
    IRoleRepository roleRepository)
{
    public async Task<OperationResult> Handle(UpdateUserSettingsRequest model)
    {
        var user = await userRepository.GetUserByIdAsync(model.Id);
        
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