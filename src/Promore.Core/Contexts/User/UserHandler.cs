using Promore.Core.Contexts.Region.Contracts;
using Promore.Core.Contexts.Role.Contracts;
using Promore.Core.Contexts.User.Contracts;
using SecureIdentity.Password;
using Responses = Promore.Core.Contexts.User.Models.Responses;
using Requests = Promore.Core.Contexts.User.Models.Requests;

namespace Promore.Core.Contexts.User;

public class UserHandler
{
    private readonly IUserRepository _userRepository;
    private readonly IRegionRepository _regionRepository;
    private readonly IRoleRepository _roleRepository;

    public UserHandler(IUserRepository userRepository, IRegionRepository regionRepository, IRoleRepository roleRepository)
    {
        _userRepository = userRepository;
        _regionRepository = regionRepository;
        _roleRepository = roleRepository;
    }
    
    public async Task<OperationResult<Entity.User>> GetUserByLoginAsync(Requests.Login model)
    { 
        var user = await _userRepository.GetUserByLogin(model); 
        
        if (user is null || !user.Active)
            return OperationResult<Entity.User>.FailureResult($"Usuário '{model.Email}' não encontrado ou não está ativo!");
        if (!PasswordHasher.Verify(user!.PasswordHash, model.Password))
             return OperationResult<Entity.User>.FailureResult("Usuário ou senha inválida!");
        
        return OperationResult<Entity.User>.SuccessResult(user);
    }
    
    public async Task<OperationResult<List<Responses.ReadUser>>> GetAllAsync()
    {
        var users = await _userRepository.GetAllAsync();
        if (users.Count == 0)
            return OperationResult<List<Responses.ReadUser>>.FailureResult("Nenhum usuário cadastrado!");
        
        return OperationResult<List<Responses.ReadUser>>.SuccessResult(users);
    }
    
    public async Task<OperationResult<long>> InsertAsync(Requests.CreateUser model)
    {
        var regions = _regionRepository.GetRegionsByIdListAsync(model.Regions).Result;
        var roles = _roleRepository.GetRolesByIdListAsync(model.Roles).Result;
        
        var user = new Entity.User
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

        var id = await _userRepository.InsertAsync(user);

        return id > 0 ? OperationResult<long>.SuccessResult(id) : OperationResult<long>.FailureResult("Não foi possível inserir o usuário!");
    }
    
    public async Task<OperationResult> UpdateSettingsAsync(Requests.UpdateSettingsUser model)
    {
        var user = await _userRepository.GetEntityByIdAsync(model.Id);
        
        if (user is null || !user.Active)
            return OperationResult.FailureResult($"Usuário '{model.Id}' não encontrado ou não está ativo!");
        
        var regions = _regionRepository.GetRegionsByIdListAsync(model.Regions).Result;
        var roles = _roleRepository.GetRolesByIdListAsync(model.Roles).Result;

        user.Active = model.Active;
        user.Roles = roles;
        user.Regions = regions;

        var rowsAffected = await _userRepository.UpdateAsync(user);

        return rowsAffected > 0 ? OperationResult.SuccessResult() : OperationResult.FailureResult("Não foi possível alterar o usuário!");
    }
    
    public async Task<OperationResult<Responses.ReadUser>> GetByIdAsync(int id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user is null)
            return OperationResult<Responses.ReadUser>.FailureResult($"Usuário '{id}' não encontrado!");
        
        return OperationResult<Responses.ReadUser>.SuccessResult(user);
    }
    
    public async Task<OperationResult<Responses.ReadUser>> GetByEmailAsync(string address)
    {
        var user = await _userRepository.GetByEmailAsync(address);
        if (user is null)
            return OperationResult<Responses.ReadUser>.FailureResult($"Usuário '{address}' não encontrado!");
        
        return OperationResult<Responses.ReadUser>.SuccessResult(user);
    }

    public async Task<OperationResult> UpdateInfoAsync(Requests.UpdateInfoUser model)
    {
        var user = await _userRepository.GetEntityByIdAsync(model.Id);
        
        if (user is null || !user.Active)
            return OperationResult.FailureResult($"Usuário '{model.Email}' não encontrado ou não está ativo!");
        
        user.Email = model.Email;
        user.PasswordHash = PasswordHasher.Hash(model.Password);
        user.Name = model.Name;
        user.Cpf = model.Cpf;
        user.Profession = model.Profession;

        var rowsAffected = await _userRepository.UpdateAsync(user);

        return rowsAffected > 0 ? OperationResult.SuccessResult() : OperationResult.FailureResult("Não foi possível alterar o usuário!");
    }
    
    public async Task<OperationResult> DeleteLotFromUserAsync(int userId, string lotId)
    {
        var user = await _userRepository.GetEntityByIdAsync(userId);
        if (user.Equals(new Entity.User()))
            return OperationResult.FailureResult($"Usuário não encontrado ou não está ativo!");

        var lot = user.Lots.FirstOrDefault(x => x.Id == lotId);
        if (lot is null)
            return OperationResult.FailureResult($"Lote não vinculado ao usuário!");

        user.Lots.Remove(lot);

        var rowsAffected = await _userRepository.UpdateAsync(user);
        return rowsAffected > 0 ? OperationResult.SuccessResult() : OperationResult.FailureResult("Não foi possível remover o lote do usuário!");
    }

}