using Promore.Core;
using Promore.Core.Contracts;
using Promore.Core.ViewModels.Responses;

namespace Promore.UseCases.Lot.UseCases;

public class GetStatusByRegion(ILotRepository lotRepository)
{
    public async Task<OperationResult<List<LotStatusResponse>>> Handle(int regionId)
    {
        var regions = await lotRepository.GetStatusByRegion(regionId);
        return OperationResult<List<LotStatusResponse>>.SuccessResult(regions);
    }
}