using Promore.Core;
using Promore.Core.Contracts;
using Promore.Core.ViewModels.Requests;
using Promore.Core.ViewModels.Responses;

namespace Promore.UseCases.Lot;

public class LotHandler(
    ILotRepository lotRepository,
    IRegionRepository regionRepository,
    IClientRepository clientRepository,
    IUserRepository userRepository)
{
    public async Task<OperationResult<List<LotStatusResponse>>> GetStatusByRegionAsync(int regionId)
        => await new UseCases.GetStatusByRegion(lotRepository).Handle(regionId);

    public async Task<OperationResult<LotResponse>> GetByIdAsync(string id)
        => await new UseCases.GetById(lotRepository).Handle(id);
    
    public async Task<OperationResult<string>> InsertAsync(LotCreateRequest model)
        => await new UseCases.Insert(lotRepository,userRepository,regionRepository,clientRepository).Handle(model);

    public async Task<OperationResult> UpdateAsync(LotUpdateRequest model)
        => await new UseCases.Update(lotRepository, userRepository, regionRepository, clientRepository).Handle(model);

    public async Task<OperationResult> DeleteAsync(string id)
        => await new UseCases.Delete(lotRepository).Handle(id);
}