using Promore.Core.Contexts.Region.Contracts;
using Promore.Core.Contexts.Role.Contracts;
using Promore.Core.Contexts.User.Contracts;
using Promore.Core.Services.Contracts;
using SecureIdentity.Password;
using Responses = Promore.Core.Contexts.User.Models.Responses;
using Requests = Promore.Core.Contexts.User.Models.Requests;

namespace Promore.Core.Contexts.User;

public class UserHandler
{
    private readonly IUserRepository _userRepository;
    private readonly IRegionRepository _regionRepository;
    private readonly IRoleRepository _roleRepository;
    private readonly ITokenService _tokenService;

    public UserHandler(IUserRepository userRepository, IRegionRepository regionRepository, IRoleRepository roleRepository, ITokenService tokenService)
    {
        _userRepository = userRepository;
        _regionRepository = regionRepository;
        _roleRepository = roleRepository;
        _tokenService = tokenService;
    }
    
    public async Task<OperationResult<string>> LoginAsync(Requests.Login model)
    { 
        var user = await _userRepository.LoginAsync(model); 
        
        if (user is null || !user.Active)
            return OperationResult<string>.FailureResult($"Usuário '{model.Email}' não encontrado ou não está ativo!");
        if (!PasswordHasher.Verify(user!.PasswordHash, model.Password))
             return OperationResult<string>.FailureResult("Usuário ou senha inválida!");

        var token = _tokenService.GenerateToken(user);
        
        return OperationResult<string>.SuccessResult(token);
    }

    public async Task<OperationResult<List<Responses.ReadUser>>> GetAllAsync()
    {
        var users = await _userRepository.GetAll();
        return OperationResult<List<Responses.ReadUser>>.SuccessResult(users);
    }

    public async Task<OperationResult<Responses.ReadUser>> GetByIdAsync(int id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        return OperationResult<Responses.ReadUser>.SuccessResult(user);
    }
    
    public async Task<OperationResult<Responses.ReadUser>> GetByEmailAddressAsync(string address)
    {
        var user = await _userRepository.GetByEmailAddress(address);
        return OperationResult<Responses.ReadUser>.SuccessResult(user);
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
    
    public async Task<OperationResult> UpdateInfoAsync(Requests.UpdateInfoUser model)
    {
        var user = await _userRepository.GetUserByIdAsync(model.Id);
        
        if (user is null || !user.Active)
            return OperationResult.FailureResult($"Usuário '{model.Email}' não encontrado ou não está ativo!");
        
        user.Email = model.Email;
        user.PasswordHash = PasswordHasher.Hash(model.Password);
        user.Name = model.Name;
        user.Cpf = model.Cpf;
        user.Profession = model.Profession;

        var rowsAffected = await _userRepository.UpdateInfoAsync(user);

        return rowsAffected > 0 ? OperationResult.SuccessResult() : OperationResult.FailureResult("Não foi possível alterar o usuário!");
    }
    
    public async Task<OperationResult> UpdateSettingsAsync(Requests.UpdateSettingsUser model)
    {
        var user = await _userRepository.GetUserByIdAsync(model.Id);
        
        if (user is null || !user.Active)
            return OperationResult.FailureResult($"Usuário não encontrado ou não está ativo!");
        
        var regions = _regionRepository.GetRegionsByIdListAsync(model.Regions).Result;
        var roles = _roleRepository.GetRolesByIdListAsync(model.Roles).Result;

        user.Active = model.Active;
        user.Roles = roles;
        user.Regions = regions;

        var rowsAffected = await _userRepository.UpdateSettingsAsync(user);

        return rowsAffected > 0 ? OperationResult.SuccessResult() : OperationResult.FailureResult("Não foi possível alterar o usuário!");
    }

    public async Task<OperationResult> DeleteAsync(int id)
    {
        var rowsAffected = await _userRepository.DeleteAsync(id);
        return rowsAffected > 0 ? OperationResult.SuccessResult("Usuário removido!") : OperationResult.FailureResult("Não foi possível apagar o usuário!");
    }
}