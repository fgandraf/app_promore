using Promore.Core;
using Promore.Core.Contracts;
using Promore.Core.ViewModels.Requests;
using SecureIdentity.Password;

namespace Promore.UseCases.User.UseCases;

public class Insert(
    IUserRepository userRepository,
    IRegionRepository regionRepository,
    IRoleRepository roleRepository)
{
    public async Task<OperationResult<long>> Handle(UserCreateRequest model)
    {
        var regions = regionRepository.GetRegionsByIdListAsync(model.Regions).Result;
        var roles = roleRepository.GetRolesByIdListAsync(model.Roles).Result;
        
        var user = new Core.Entities.User
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