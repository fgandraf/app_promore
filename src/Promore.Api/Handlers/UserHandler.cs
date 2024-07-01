using Microsoft.EntityFrameworkCore;
using Promore.Api.Data;
using Promore.Core.Handlers;
using Promore.Core.Models;
using Promore.Core.Requests.Users;
using Promore.Core.Responses;
using Promore.Core.Responses.Users;
using SecureIdentity3.Password;

namespace Promore.Api.Handlers;

public class UserHandler(PromoreDataContext context) : IUserHandler
{
    public async Task<Response<GetUserResponse?>> CreateAsync(CreateUserRequest request)
    {
        try
        {
            var regions = await context
                .Regions
                .Where(region => request.Regions.Contains(region.Id))
                .ToListAsync();
            if (regions.Count == 0)
                return new Response<GetUserResponse?>(null, 404, $"Regiões não encontradas!");
        
            var roles = await context
                .Roles
                .Where(role => request.Roles.Contains(role.Id))
                .ToListAsync();
            if (roles.Count == 0)
                return new Response<GetUserResponse?>(null, 404, $"Papéis não encontrados!");
        
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

            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();
            
            var userResponse = new GetUserResponse(
                Id: user.Id,
                Active: request.Active,
                Email: request.Email,
                Name: request.Name,
                Cpf: request.Cpf,
                Profession: request.Profession,
                Roles: request.Roles,
                Regions: request.Regions,
                Lots: []
            );
            
            return new Response<GetUserResponse?>(userResponse);
        }
        catch
        {
            return new Response<GetUserResponse?>(null, 500, "[AHUCR12] Não foi possível criar o usuário!");
        }
    }
    
    public async Task<Response<UpdateUserSettingsResponse?>> UpdateSettingsAsync(UpdateUserSettingsRequest request)
    {
        try
        {
            var user = await context
                .Users
                .Include(roles => roles.Roles)
                .Include(regions => regions.Regions)
                .Include(lots => lots.Lots)
                .FirstOrDefaultAsync(x => x.Id == request.Id);
            if (user is null || !user.Active)
                return new Response<UpdateUserSettingsResponse?>(null, 404, $"Usuário '{request.Id}' não encontrado ou não ativo!");
            
            var regions = await context
                .Regions
                .Where(region => request.Regions.Contains(region.Id))
                .ToListAsync();
        
            var roles = await context
                .Roles
                .Where(role => request.Roles.Contains(role.Id))
                .ToListAsync();
            if (roles.Count == 0)
                return new Response<UpdateUserSettingsResponse?>(null, 404, $"Papéis não encontrados!");
            
            user.Active = request.Active;
            user.Roles = roles;
            user.Regions = regions;
            
            context.Update(user);
            await context.SaveChangesAsync();

            var userResponse = new UpdateUserSettingsResponse(
                Id: user.Id,
                Active: user.Active,
                Roles: request.Roles,
                Regions: request.Regions
            );
            
            return new Response<UpdateUserSettingsResponse?>(userResponse);
        }
        catch
        {
            return new Response<UpdateUserSettingsResponse?>(null, 500, "[AHUUP12] Não foi possível alterar o usuário!");
        }
        
    }
    
    public async Task<Response<UpdateUserInfoResponse?>> UpdateInfoAsync(UpdateUserInfoRequest request)
    {
        try
        {
            var user = await context
                .Users
                .Include(roles => roles.Roles)
                .Include(regions => regions.Regions)
                .Include(lots => lots.Lots)
                .FirstOrDefaultAsync(x => x.Id == request.Id);
            if (user is null || !user.Active)
                return new Response<UpdateUserInfoResponse?>(null, 404, $"Usuário '{request.Id}' não encontrado ou não ativo!");
        
            user.Email = request.Email;
            user.PasswordHash = PasswordHasher.Hash(request.Password);
            user.Name = request.Name;
            user.Cpf = request.Cpf;
            user.Profession = request.Profession;

            context.Update(user);
            await context.SaveChangesAsync();

            var userResponse = new UpdateUserInfoResponse(
                Id: user.Id,
                Email: user.Email,
                Name: user.Name,
                Cpf: user.Cpf,
                Profession: user.Profession
            );
            
            return new Response<UpdateUserInfoResponse?>(userResponse);
        }
        catch
        {
            return new Response<UpdateUserInfoResponse?>(null, 500, "[AHUUP13] Não foi possível alterar o usuário!");
        }
        
    }
    
    public async Task<Response<RemoveLotFromUserResponse?>> RemoveLotFromUserAsync(RemoveLotFromUserRequest request)
    {
        try
        {
            var user = await context
                .Users
                .Include(roles => roles.Roles)
                .Include(regions => regions.Regions)
                .Include(lots => lots.Lots)
                .FirstOrDefaultAsync(x => x.Id == request.Id);
            if (user is null || !user.Active)
                return new Response<RemoveLotFromUserResponse?>(null, 404, $"Usuário '{request.Id}' não encontrado ou não ativo!");

            var lot = user.Lots.FirstOrDefault(x => x.Id == request.LotId);
            if (lot is null)
                return new Response<RemoveLotFromUserResponse?>(null, 404, "Lote não vinculado ao usuário!");
            
            user.Lots.Remove(lot);
            context.Update(user);
            await context.SaveChangesAsync();
            
            var userResponse = new RemoveLotFromUserResponse(
                Id: user.Id,
                LotsId: user.Lots.Select(x => x.Id).ToList()
            );
            
            return new Response<RemoveLotFromUserResponse?>(userResponse);
        }
        catch
        {
            return new Response<RemoveLotFromUserResponse?>(null, 500, "[AHUUP14] Não foi possível remover o lote do usuário o usuário!");
        }
    }
    
    public async Task<Response<GetUserResponse?>> GetByIdAsync(GetUserByIdRequest request)
    {
        try
        {
            var user = await context
                .Users
                .AsNoTracking()
                .Where(x => x.Id == request.Id)
                .Include(roles => roles.Roles)
                .Include(regions => regions.Regions)
                .Include(lots => lots.Lots)
                .Select(user => new GetUserResponse
                (
                    user.Id,
                    user.Active,
                    user.Email,
                    user.Name,
                    user.Cpf,
                    user.Profession,
                    user.Roles.Select(x => x.Id).ToList(),
                    user.Regions.Select(x => x.Id).ToList(),
                    user.Lots.Select(x => x.Id).ToList()
                ))
                .FirstOrDefaultAsync();
            if (user is null)
                return new Response<GetUserResponse?>(null, 404, $"Usuário '{request.Id}' não encontrado!");

            return new Response<GetUserResponse?>(user);
        }
        catch
        {
            return new Response<GetUserResponse?>(null, 500, "[AHUGT12] Não foi possível encontrar o usuário!");
        }
    }
    
    public async Task<Response<GetUserResponse?>> GetByEmailAsync(GetUserByEmailRequest request)
    {
        try
        {
            var user = await context
                .Users
                .AsNoTracking()
                .Where(x => x.Email == request.Email)
                .Include(roles => roles.Roles)
                .Include(regions => regions.Regions)
                .Include(lots => lots.Lots)
                .Select(user => new GetUserResponse
                (
                    user.Id,
                    user.Active,
                    user.Email,
                    user.Name,
                    user.Cpf,
                    user.Profession,
                    user.Roles.Select(x => x.Id).ToList(),
                    user.Regions.Select(x => x.Id).ToList(),
                    user.Lots.Select(x => x.Id).ToList()
                ))
                .FirstOrDefaultAsync();
            if (user is null)
                return new Response<GetUserResponse?>(null, 404, $"Usuário '{request.Email}' não encontrado!");

            return new Response<GetUserResponse?>(user);
        }
        catch
        {
            return new Response<GetUserResponse?>(null, 500, "[AHUGT13] Não foi possível encontrar o usuário!");
        }
    }
    
    public async Task<Response<User?>> GetUserByLoginAsync(GetUserByLoginRequest request)
    {
        try
        {
            var user = await context
                .Users
                .AsNoTracking()
                .Include(x => x.Roles)
                .FirstOrDefaultAsync(x => x.Email == request.Email);

            if (user is null || !user.Active)
                return new Response<User?>(null, 404, $"Usuário '{request.Email}' não encontrado ou não está ativo!");

            if (!PasswordHasher.Verify(user.PasswordHash, request.Password))
                return new Response<User?>(null, 400, "Usuário ou senha inválida!");

            return new Response<User?>(user);
        }
        catch
        {
            return new Response<User?>(null, 500, "[AHUGT14] Não foi possível encontrar o usuário!");
        }
    }
    
    public async Task<Response<List<GetUsersResponse>?>> GetAllAsync(GetAllUsersRequest request)
    {
        try
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
                    user.Regions.Select(x => x.Id).ToList(),
                    user.Lots.Select(x => x.Id).ToList()
                ))
                .Skip(request.PageNumber * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();

            if (users.Count == 0)
                return new Response<List<GetUsersResponse>?>(null, 404, "Nenhum usuário cadastrado!");

            return new Response<List<GetUsersResponse>?>(users);
        }
        catch
        {
            return new Response<List<GetUsersResponse>?>(null, 500, "[AHUGA12] Não foi possível encontrar usuários!");
        }
    }
}