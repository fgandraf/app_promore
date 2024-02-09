using Promore.Core.Contexts.LotContext.Contracts;

namespace Promore.Core.Contexts.LotContext.UseCases.GetStatusByRegion;

public class Handler(ILotRepository lotRepository)
{
    public async Task<OperationResult<List<Response>>> Handle(int regionId)
    {
        var regions = await lotRepository.GetStatusByRegion(regionId);
        return OperationResult<List<Response>>.SuccessResult(regions);
    }
}