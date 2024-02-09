using Promore.Core.Contexts.RegionContext.Contracts;
using Promore.Core.Contexts.RoleContext.Contracts;
using Promore.Core.Contexts.UserContext.Contracts;
using Promore.Core.Contexts.UserContext.Entities;

namespace Promore.Core.Contexts.UserContext;

public class UserHandler(
    IUserRepository userRepository,
    IRegionRepository regionRepository,
    IRoleRepository roleRepository)
{
    public async Task<OperationResult<User>> GetUserByLoginAsync(UseCases.GetByLogin.LoginRequest model)
        => await new UseCases.GetByLogin.Handler(userRepository).Handle(model);
    
    public async Task<OperationResult<List<UseCases.GetAll.Response>>> GetAllAsync()
        => await new UseCases.GetAll.Handler(userRepository).Handle();
    
    public async Task<OperationResult<long>> CreateAsync(UseCases.Create.CreateUserRequest model)
        => await new UseCases.Create.Handler(userRepository,regionRepository,roleRepository).Handle(model); 
    
    public async Task<OperationResult> UpdateSettingsAsync(UseCases.UpdateSettings.UpdateUserSettingsRequest model)
        => await new UseCases.UpdateSettings.Handler(userRepository,regionRepository,roleRepository).Handle(model); 
    
    public async Task<OperationResult<UseCases.GetById.Response>> GetByIdAsync(int id)
        => await new UseCases.GetById.Handler(userRepository).Handle(id); 
    
    public async Task<OperationResult<UseCases.GetByEmail.Response>> GetByEmailAsync(string address)
        => await new UseCases.GetByEmail.Handler(userRepository).Handle(address); 

    public async Task<OperationResult> UpdateInfoAsync(UseCases.UpdateInfo.UpdateUserInfoRequest model)
        => await new UseCases.UpdateInfo.Handler(userRepository).Handle(model); 
    
    public async Task<OperationResult> DeleteLotFromUserAsync(int userId, string lotId)
        => await new UseCases.DeleteLotFromUser.Handler(userRepository).Handle(userId,lotId); 

}