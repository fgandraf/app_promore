using Promore.Core.Models;
using Promore.Core.Requests.Users;
using Promore.Core.Responses.Users;

namespace Promore.Core.Handlers;

public interface IUserHandler
{
    Task<OperationResult<User>> GetUserByLoginAsync(GetUserByLoginRequest request);
    Task<OperationResult<List<GetUsersResponse>>> GetAllAsync(GetAllUsersRequest request);
    Task<OperationResult<GetUserByIdResponse>> GetByIdAsync(GetUserByIdRequest request);
    Task<OperationResult<GetUserByEmailResponse>> GetByEmailAsync(GetUserByEmailRequest request);
    Task<OperationResult<long>> CreateAsync(CreateUserRequest request);
    Task<OperationResult> UpdateSettingsAsync(UpdateUserSettingsRequest request);
    Task<OperationResult> UpdateInfoAsync(UpdateUserInfoRequest request);
    Task<OperationResult> RemoveLotFromUserAsync(RemoveLotFromUserRequest request);
}