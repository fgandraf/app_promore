using Promore.Core;
using Promore.Core.Contracts;
using Promore.UseCases.User.UseCases;
using Requests = Promore.Core.ViewModels.Requests;
using Response = Promore.Core.ViewModels.Responses;

namespace Promore.UseCases.User;

public class UserHandler(
    IUserRepository userRepository,
    IRegionRepository regionRepository,
    IRoleRepository roleRepository)
{
    public async Task<OperationResult<Core.Entities.User>> GetUserByLoginAsync(Requests.LoginRequest model)
        => await new GetUserByLogin(userRepository).Handle(model);
    
    public async Task<OperationResult<List<Response.UserResponse>>> GetAllAsync()
        => await new GetAll(userRepository).Handle();
    
    public async Task<OperationResult<long>> InsertAsync(Requests.UserCreateRequest model)
        => await new Insert(userRepository,regionRepository,roleRepository).Handle(model); 
    
    public async Task<OperationResult> UpdateSettingsAsync(Requests.UserUpdateSettingsRequest model)
        => await new UpdateSettings(userRepository,regionRepository,roleRepository).Handle(model); 
    
    public async Task<OperationResult<Response.UserResponse>> GetByIdAsync(int id)
        => await new GetById(userRepository).Handle(id); 
    
    public async Task<OperationResult<Response.UserResponse>> GetByEmailAsync(string address)
        => await new GetByEmail(userRepository).Handle(address); 

    public async Task<OperationResult> UpdateInfoAsync(Requests.UserUpdateInfoRequest model)
        => await new UpdateInfo(userRepository).Handle(model); 
    
    public async Task<OperationResult> DeleteLotFromUserAsync(int userId, string lotId)
        => await new DeleteLotFromUser(userRepository).Handle(userId,lotId); 

}