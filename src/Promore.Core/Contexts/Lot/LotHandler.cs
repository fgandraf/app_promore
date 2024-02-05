using Promore.Core.Contexts.Client.Contracts;
using Promore.Core.Contexts.Lot.Contracts;
using Promore.Core.Contexts.Region.Contracts;
using Promore.Core.Contexts.User.Contracts;
using Responses = Promore.Core.Contexts.Lot.Models.Responses;
using Requests = Promore.Core.Contexts.Lot.Models.Requests;

namespace Promore.Core.Contexts.Lot;

public class LotHandler
{
    private readonly ILotRepository _lotRepository;
    private readonly IRegionRepository _regionRepository;
    private readonly IClientRepository _clientRepository;
    private readonly IUserRepository _userRepository;
    

    public LotHandler(ILotRepository lotRepository, IRegionRepository regionRepository, IClientRepository clientRepository, IUserRepository userRepository)
    {
        _lotRepository = lotRepository;
        _regionRepository = regionRepository;
        _clientRepository = clientRepository;
        _userRepository = userRepository;
    }

    public async Task<OperationResult<List<Responses.ReadLot>>> GetAllAsync()
    {
        var regions = await _lotRepository.GetAll();
        return OperationResult<List<Responses.ReadLot>>.SuccessResult(regions);
    }
    
    public async Task<OperationResult<List<Responses.ReadStatusLot>>> GetStatusByRegionAsync(int regionId)
    {
        var regions = await _lotRepository.GetStatusByRegion(regionId);
        return OperationResult<List<Responses.ReadStatusLot>>.SuccessResult(regions);
    }
    
    
    public async Task<OperationResult<Responses.ReadLot>> GetByIdAsync(string id)
    {
        var region = await _lotRepository.GetByIdAsync(id);
        return OperationResult<Responses.ReadLot>.SuccessResult(region);
    }
    
    
    public async Task<OperationResult<string>> InsertAsync(Requests.CreateLot model)
    {
        var lot = new Entity.Lot
        {
            Block = model.Block,
            Number = model.Number,
            SurveyDate = model.SurveyDate,
            LastModifiedDate = model.LastModifiedDate,
            Status = model.Status,
            Comments = model.Comments,
            User = _userRepository.GetUserByIdAsync(model.UserId).Result,
            Region = _regionRepository.GetRegionByIdAsync(model.RegionId).Result,
            Clients = _clientRepository.GetClientsByIdListAsync(model.Clients).Result
        };
        
        var id = await _lotRepository.InsertAsync(lot);

        return string.IsNullOrEmpty(id) ? OperationResult<string>.SuccessResult(id) : OperationResult<string>.FailureResult("Não foi possível inserir o lote!");
    }
    
    
    public async Task<OperationResult> UpdateAsync(Requests.UpdateLot model)
    {
        var lot = await _lotRepository.GetLotById(model.Id);
        if (lot is null)
            return OperationResult.FailureResult($"Lote '{model.Id}' não encontrado!");

        var user = _userRepository.GetUserByIdAsync(model.UserId).Result;
        if (user is null)
            return OperationResult.FailureResult($"Usuário '{model.UserId}' não encontrado!");
        
        var region = _regionRepository.GetRegionByIdAsync(model.RegionId).Result;
        if (region is null)
            return OperationResult.FailureResult($"Região '{model.RegionId}' não encontrada!");
        
        var clients = _clientRepository.GetClientsByIdListAsync(model.Clients).Result;
        if (clients is null)
            return OperationResult.FailureResult($"Clientes não encontrados!");
        
        lot.Block = lot.Block;
        lot.Number = lot.Number;
        lot.SurveyDate = lot.SurveyDate;
        lot.LastModifiedDate = lot.LastModifiedDate;
        lot.Status = lot.Status;
        lot.Comments = lot.Comments;
        lot.User = user;
        lot.Region = region;
        lot.Clients = clients;
        
        
        var rowsAffected = await _lotRepository.UpdateAsync(lot);

        return rowsAffected > 0 ? OperationResult.SuccessResult() : OperationResult.FailureResult("Não foi possível alterar o lote!");
    }
    
    public async Task<OperationResult> DeleteAsync(string id)
    {
        var rowsAffected = await _lotRepository.DeleteAsync(id);
        return rowsAffected > 0 ? OperationResult.SuccessResult("Lote removido!") : OperationResult.FailureResult("Não foi possível apagar o lote!");
    }
}