using Promore.Core.Models;
using Promore.Core.Requests.Users;
using Promore.Core.Responses;
using Promore.Core.Responses.Users;

namespace Promore.Core.Handlers;

public interface IUserHandler
{
    Task<Response<GetUserResponse?>> CreateAsync(CreateUserRequest request);
    Task<Response<UpdateUserSettingsResponse?>> UpdateSettingsAsync(UpdateUserSettingsRequest request);
    Task<Response<UpdateUserInfoResponse?>> UpdateInfoAsync(UpdateUserInfoRequest request);
    Task<Response<RemoveLotFromUserResponse?>> RemoveLotFromUserAsync(RemoveLotFromUserRequest request);
    Task<Response<GetUserResponse?>> GetByIdAsync(GetUserByIdRequest request);
    Task<Response<GetUserResponse?>> GetByEmailAsync(GetUserByEmailRequest request);
    Task<Response<User?>> GetUserByLoginAsync(GetUserByLoginRequest request);
    Task<Response<List<GetUsersResponse>?>> GetAllAsync(GetAllUsersRequest request);
}