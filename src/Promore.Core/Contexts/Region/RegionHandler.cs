using Promore.Core.Contexts.Region.Contracts;
using Promore.Core.Contexts.User.Contracts;
using Requests = Promore.Core.Contexts.Region.Models.Requests;
using Responses = Promore.Core.Contexts.Region.Models.Responses;

namespace Promore.Core.Contexts.Region;

public class RegionHandler
{
    private readonly IRegionRepository _regionRepository;
    private readonly IUserRepository _userRepository;

    public RegionHandler(IRegionRepository regionRepository, IUserRepository userRepository)
    {
        _regionRepository = regionRepository;
        _userRepository = userRepository;
    }
    
    public async Task<OperationResult<List<Responses.ReadRegion>>> GetAllAsync()
    {
        var regions = await _regionRepository.GetAll();
        if (regions.Count == 0)
            return OperationResult<List<Responses.ReadRegion>>.FailureResult("Nenhuma região cadastrada!");
        
        return OperationResult<List<Responses.ReadRegion>>.SuccessResult(regions);
    }

    public async Task<OperationResult<Responses.ReadRegion>> GetByIdAsync(int id)
    {
        var region = await _regionRepository.GetByIdAsync(id);
        if (region is null)
            return OperationResult<Responses.ReadRegion>.FailureResult($"Região '{id}' não encontrada!");
        
        return OperationResult<Responses.ReadRegion>.SuccessResult(region);
    }
    
    public async Task<OperationResult<long>> InsertAsync(Requests.CreateRegion model)
    {
        var users = _userRepository.GetEntitiesByIdsAsync(model.Users).Result;
        
        var region = new Entity.Region
        {
            Name = model.Name,
            EstablishedDate = model.EstablishedDate,
            StartDate = model.StartDate,
            EndDate = model.EndDate,
            Users = users
        };
        
        var id = await _regionRepository.InsertAsync(region);

        return id > 0 ? OperationResult<long>.SuccessResult(id) : OperationResult<long>.FailureResult("Não foi possível inserir a região!");
    }
    
    public async Task<OperationResult> UpdateAsync(Requests.UpdateRegion model)
    {
        var region = await _regionRepository.GetRegionByIdAsync(model.Id);
        
        region.Name = model.Name;
        region.EstablishedDate = model.EstablishedDate;
        region.StartDate = model.StartDate;
        region.EndDate = model.EndDate;
        
        var rowsAffected = await _regionRepository.UpdateAsync(region);

        return rowsAffected > 0 ? OperationResult.SuccessResult() : OperationResult.FailureResult("Não foi possível alterar a região!");
    }
    
    public async Task<OperationResult> DeleteAsync(int id)
    {
        var rowsAffected = await _regionRepository.DeleteAsync(id);
        return rowsAffected > 0 ? OperationResult.SuccessResult("Região removida!") : OperationResult.FailureResult("Não foi possível apagar a região!");
    }

}