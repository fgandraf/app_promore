using Promore.Core.Models;
using Promore.Core.Requests.Users;
using Promore.Core.Responses;
using Promore.Core.Responses.Users;

namespace Promore.Core.Handlers;

public interface IUserHandler
{
    Task<Response<GetUserByIdResponse?>> CreateAsync(CreateUserRequest request);
    Task<Response<UpdateUserSettingsResponse?>> UpdateSettingsAsync(UpdateUserSettingsRequest request);
    Task<Response<UpdateUserInfoResponse?>> UpdateInfoAsync(UpdateUserInfoRequest request);
    Task<Response<RemoveLotFromUserResponse?>> RemoveLotFromUserAsync(RemoveLotFromUserRequest request);
    
    
    
    
    
    
    Task<OperationResult<User>> GetUserByLoginAsync(GetUserByLoginRequest request);
    Task<OperationResult<List<GetUsersResponse>>> GetAllAsync(GetAllUsersRequest request);
    Task<OperationResult<GetUserByIdResponse>> GetByIdAsync(GetUserByIdRequest request);
    Task<OperationResult<GetUserByEmailResponse>> GetByEmailAsync(GetUserByEmailRequest request);
    
}