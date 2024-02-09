using Promore.Core.Contexts.ClientContext.Contracts;
using Promore.Core.Contexts.LotContext.Contracts;
using Promore.Core.Contexts.RegionContext.Contracts;
using Promore.Core.Contexts.UserContext.Contracts;

namespace Promore.Core.Contexts.LotContext;

public class LotHandler(
    ILotRepository lotRepository,
    IRegionRepository regionRepository,
    IClientRepository clientRepository,
    IUserRepository userRepository)
{
    public async Task<OperationResult<List<UseCases.GetStatusByRegion.Response>>> GetStatusByRegionAsync(int regionId)
        => await new UseCases.GetStatusByRegion.Handler(lotRepository).Handle(regionId);

    public async Task<OperationResult<UseCases.GetById.Response>> GetByIdAsync(string id)
        => await new UseCases.GetById.Handler(lotRepository).Handle(id);
    
    public async Task<OperationResult<string>> CreateAsync(UseCases.Create.CreateLotRequest model)
        => await new UseCases.Create.Hander(lotRepository,userRepository,regionRepository,clientRepository).Handle(model);

    public async Task<OperationResult> UpdateAsync(UseCases.Update.UpdateLotRequest model)
        => await new UseCases.Update.Handler(lotRepository, userRepository, regionRepository, clientRepository).Handle(model);

    public async Task<OperationResult> DeleteAsync(string id)
        => await new UseCases.Delete.Handler(lotRepository).Handle(id);
}