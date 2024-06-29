using Promore.Core;
using Promore.Core.Handlers;
using Promore.Core.Models;
using Promore.Core.Requests.Users;
using Promore.Core.Responses.Users;
using SecureIdentity.Password;

namespace Promore.Api.Services;

public class UserService(
    IUserHandler userHandler,
    IRegionHandler regionHandler,
    IRoleHandler roleHandler)
{
    public async Task<OperationResult<User>> GetUserByLoginAsync(GetUserByLoginRequest model)
    {
        var user = await userHandler.GetUserByLogin(model); 
        
        if (user is null || !user.Active)
            return OperationResult<User>.FailureResult($"Usuário '{model.Email}' não encontrado ou não está ativo!");
        if (!PasswordHasher.Verify(user!.PasswordHash, model.Password))
            return OperationResult<User>.FailureResult("Usuário ou senha inválida!");
        
        return OperationResult<User>.SuccessResult(user);
    }

    public async Task<OperationResult<List<GetUsersResponse>>> GetAllAsync()
    {
        var users = await userHandler.GetAllAsync();
        if (users.Count == 0)
            return OperationResult<List<GetUsersResponse>>.FailureResult("Nenhum usuário cadastrado!");
        
        return OperationResult<List<GetUsersResponse>>.SuccessResult(users);
    }

    public async Task<OperationResult<long>> CreateAsync(CreateUserRequest model)
    {
        var regions = regionHandler.GetRegionsByIdListAsync(model.Regions).Result;
        var roles = roleHandler.GetRolesByIdListAsync(model.Roles).Result;
        
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

        var id = await userHandler.InsertAsync(user);

        return id > 0 ? OperationResult<long>.SuccessResult(id) : OperationResult<long>.FailureResult("Não foi possível inserir o usuário!");
    }

    public async Task<OperationResult> UpdateSettingsAsync(UpdateUserSettingsRequest model)
    {
        var user = await userHandler.GetUserByIdAsync(model.Id);
        
        if (user is null || !user.Active)
            return OperationResult.FailureResult($"Usuário '{model.Id}' não encontrado ou não está ativo!");
        
        var regions = regionHandler.GetRegionsByIdListAsync(model.Regions).Result;
        var roles = roleHandler.GetRolesByIdListAsync(model.Roles).Result;

        user.Active = model.Active;
        user.Roles = roles;
        user.Regions = regions;

        var rowsAffected = await userHandler.UpdateAsync(user);

        return rowsAffected > 0 ? OperationResult.SuccessResult() : OperationResult.FailureResult("Não foi possível alterar o usuário!");
    }

    public async Task<OperationResult<GetUserByIdResponse>> GetByIdAsync(int id)
    {
        var user = await userHandler.GetByIdAsync(id);
        if (user is null)
            return OperationResult<GetUserByIdResponse>.FailureResult($"Usuário '{id}' não encontrado!");
        
        return OperationResult<GetUserByIdResponse>.SuccessResult(user);
    }

    public async Task<OperationResult<GetUserByEmailResponse>> GetByEmailAsync(string address)
    {
        var user = await userHandler.GetByEmailAsync(address);
        if (user is null)
            return OperationResult<GetUserByEmailResponse>.FailureResult($"Usuário '{address}' não encontrado!");
        
        return OperationResult<GetUserByEmailResponse>.SuccessResult(user);
    }

    public async Task<OperationResult> UpdateInfoAsync(UpdateUserInfoRequest model)
    {
        var user = await userHandler.GetUserByIdAsync(model.Id);
        
        if (user is null || !user.Active)
            return OperationResult.FailureResult($"Usuário '{model.Email}' não encontrado ou não está ativo!");
        
        user.Email = model.Email;
        user.PasswordHash = PasswordHasher.Hash(model.Password);
        user.Name = model.Name;
        user.Cpf = model.Cpf;
        user.Profession = model.Profession;

        var rowsAffected = await userHandler.UpdateAsync(user);

        return rowsAffected > 0 ? OperationResult.SuccessResult() : OperationResult.FailureResult("Não foi possível alterar o usuário!");
    }

    public async Task<OperationResult> RemoveLotFromUserAsync(int userId, string lotId)
    {
        var user = await userHandler.GetUserByIdAsync(userId);
        if (user is null)
            return OperationResult.FailureResult($"Usuário não encontrado ou não está ativo!");

        var lot = user.Lots.FirstOrDefault(x => x.Id == lotId);
        if (lot is null)
            return OperationResult.FailureResult($"Lote não vinculado ao usuário!");

        user.Lots.Remove(lot);

        var rowsAffected = await userHandler.UpdateAsync(user);
        return rowsAffected > 0 ? OperationResult.SuccessResult() : OperationResult.FailureResult("Não foi possível remover o lote do usuário!");
    }
}