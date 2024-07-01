using Microsoft.EntityFrameworkCore;
using Promore.Api.Data;
using Promore.Core;
using Promore.Core.Handlers;
using Promore.Core.Models;
using Promore.Core.Requests.Users;
using Promore.Core.Responses.Users;
using SecureIdentity3.Password;

namespace Promore.Api.Handlers;

public class UserHandler(PromoreDataContext context) : IUserHandler
{
    public async Task<OperationResult<User>> GetUserByLoginAsync(GetUserByLoginRequest request)
    {
        var user = await context
            .Users
            .AsNoTracking()
            .Include(x => x.Roles)
            .FirstOrDefaultAsync(x => x.Email == request.Email);
        
        if (user is null || !user.Active)
            return OperationResult<User>.FailureResult($"Usuário '{request.Email}' não encontrado ou não está ativo!");
        
        if (!PasswordHasher.Verify(user!.PasswordHash, request.Password))
            return OperationResult<User>.FailureResult("Usuário ou senha inválida!");
        
        return OperationResult<User>.SuccessResult(user);
    }

    public async Task<OperationResult<List<GetUsersResponse>>> GetAllAsync(GetAllUsersRequest request)
    {
        var users = await context
            .Users
            .AsNoTracking()
            .Include(roles => roles.Roles)
            .Include(regions => regions.Regions)
            .Include(lots => lots.Lots)
            .Select(user => new GetUsersResponse
            (
                user.Id,
                user.Active,
                user.Email,
                user.Name,
                user.Cpf,
                user.Profession, 
                user.Roles.Select(x => x.Id).ToList(),
                user.Regions.Select(x=> x.Id).ToList(),
                user.Lots.Select(x=> x.Id).ToList()
            ))
            .ToListAsync();
        
        if (users.Count == 0)
            return OperationResult<List<GetUsersResponse>>.FailureResult("Nenhum usuário cadastrado!");
        
        return OperationResult<List<GetUsersResponse>>.SuccessResult(users);
    }

    public async Task<OperationResult<long>> CreateAsync(CreateUserRequest request)
    {
        var regions = await context
             .Regions
             .Where(region => request.Regions.Contains(region.Id))
             .ToListAsync();
        
        var roles = await context
            .Roles
            .Where(role => request.Roles.Contains(role.Id))
            .ToListAsync();
        
        var user = new User
        {
            Active = request.Active,
            Email = request.Email,
            PasswordHash = PasswordHasher.Hash(request.Password),
            Name = request.Name,
            Cpf = request.Cpf,
            Profession = request.Profession,
            Roles = roles,
            Regions = regions
        };

        context.Users.Add(user);
        var rowsAffected = await context.SaveChangesAsync();
        var id = rowsAffected > 0 ? user.Id : 0;

        return id > 0 ? OperationResult<long>.SuccessResult(id) : OperationResult<long>.FailureResult("Não foi possível inserir o usuário!");
    }

    public async Task<OperationResult> UpdateSettingsAsync(UpdateUserSettingsRequest request)
    {
        var user = await context
            .Users
            .Include(roles => roles.Roles)
            .Include(regions => regions.Regions)
            .Include(lots => lots.Lots)
            .FirstOrDefaultAsync(x => x.Id == request.Id);
        
        if (user is null || !user.Active)
            return OperationResult.FailureResult($"Usuário '{request.Id}' não encontrado ou não está ativo!");
        
        var regions = await context
            .Regions
            .Where(region => request.Regions.Contains(region.Id))
            .ToListAsync();
        
        var roles = await context
            .Roles
            .Where(role => request.Roles.Contains(role.Id))
            .ToListAsync();

        user.Active = request.Active;
        user.Roles = roles;
        user.Regions = regions;

        
        context.Update(user);
        var rowsAffected = await context.SaveChangesAsync();

        return rowsAffected > 0 ? OperationResult.SuccessResult() : OperationResult.FailureResult("Não foi possível alterar o usuário!");
    }

    public async Task<OperationResult<GetUserByIdResponse>> GetByIdAsync(GetUserByIdRequest request)
    {
        var user = await context
            .Users
            .AsNoTracking()
            .Where(x => x.Id == request.Id)
            .Include(roles => roles.Roles)
            .Include(regions => regions.Regions)
            .Include(lots => lots.Lots)
            .Select(user => new GetUserByIdResponse
            (
                user.Id,
                user.Active,
                user.Email,
                user.Name,
                user.Cpf,
                user.Profession, 
                user.Roles.Select(x => x.Id).ToList(),
                user.Regions.Select(x=> x.Id).ToList(),
                user.Lots.Select(x=> x.Id).ToList()
            ))
            .FirstOrDefaultAsync();
        
        if (user is null)
            return OperationResult<GetUserByIdResponse>.FailureResult($"Usuário '{request.Id}' não encontrado!");
        
        return OperationResult<GetUserByIdResponse>.SuccessResult(user);
    }

    public async Task<OperationResult<GetUserByEmailResponse>> GetByEmailAsync(GetUserByEmailRequest request)
    {
        var user = await context
            .Users
            .AsNoTracking()
            .Where(x => x.Email == request.Email)
            .Include(roles => roles.Roles)
            .Include(regions => regions.Regions)
            .Include(lots => lots.Lots)
            .Select(user => new GetUserByEmailResponse
            (
                user.Id,
                user.Active,
                user.Email,
                user.Name,
                user.Cpf,
                user.Profession, 
                user.Roles.Select(x => x.Id).ToList(),
                user.Regions.Select(x=> x.Id).ToList(),
                user.Lots.Select(x=> x.Id).ToList()
            ))
            .FirstOrDefaultAsync();
        
        if (user is null)
            return OperationResult<GetUserByEmailResponse>.FailureResult($"Usuário '{request.Email}' não encontrado!");
        
        return OperationResult<GetUserByEmailResponse>.SuccessResult(user);
    }

    public async Task<OperationResult> UpdateInfoAsync(UpdateUserInfoRequest request)
    {
        var user = await context
            .Users
            .Include(roles => roles.Roles)
            .Include(regions => regions.Regions)
            .Include(lots => lots.Lots)
            .FirstOrDefaultAsync(x => x.Id == request.Id);
        
        if (user is null || !user.Active)
            return OperationResult.FailureResult($"Usuário '{request.Email}' não encontrado ou não está ativo!");
        
        user.Email = request.Email;
        user.PasswordHash = PasswordHasher.Hash(request.Password);
        user.Name = request.Name;
        user.Cpf = request.Cpf;
        user.Profession = request.Profession;

        context.Update(user);
        var rowsAffected = await context.SaveChangesAsync();

        return rowsAffected > 0 ? OperationResult.SuccessResult() : OperationResult.FailureResult("Não foi possível alterar o usuário!");
    }

    public async Task<OperationResult> RemoveLotFromUserAsync(RemoveLotFromUserRequest request)
    {
        var user = await context
            .Users
            .Include(roles => roles.Roles)
            .Include(regions => regions.Regions)
            .Include(lots => lots.Lots)
            .FirstOrDefaultAsync(x => x.Id == request.Id);
        if (user is null)
            return OperationResult.FailureResult($"Usuário não encontrado ou não está ativo!");

        var lot = user.Lots.FirstOrDefault(x => x.Id == request.LotId);
        if (lot is null)
            return OperationResult.FailureResult($"Lote não vinculado ao usuário!");

        user.Lots.Remove(lot);

        context.Update(user);
        var rowsAffected = await context.SaveChangesAsync();

        return rowsAffected > 0 ? OperationResult.SuccessResult() : OperationResult.FailureResult("Não foi possível alterar o usuário!");
    }
}