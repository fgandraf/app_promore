using Promore.Core.Contexts.RegionContext.Contracts;
using Promore.Core.Contexts.RoleContext.Contracts;
using Promore.Core.Contexts.UserContext.Contracts;
using Promore.Core.Contexts.UserContext.Entities;
using SecureIdentity.Password;

namespace Promore.Core.Contexts.UserContext.UseCases.Create;

public class Handler(
    IUserRepository userRepository,
    IRegionRepository regionRepository,
    IRoleRepository roleRepository)
{
    public async Task<OperationResult<long>> Handle(CreateUserRequest model)
    {
        var regions = regionRepository.GetRegionsByIdListAsync(model.Regions).Result;
        var roles = roleRepository.GetRolesByIdListAsync(model.Roles).Result;
        
        var user = new User
        {
            Active = model.Active,
            Email = model.Email,
            PasswordHash = PasswordHasher.Hash(model.Password),
            Name = model.Name,
            Cpf = model.Cpf,
            Profession = model.Profession,
            Roles = roles,
            Regions = regions
        };

        var id = await userRepository.InsertAsync(user);

        return id > 0 ? OperationResult<long>.SuccessResult(id) : OperationResult<long>.FailureResult("Não foi possível inserir o usuário!");
    }
}