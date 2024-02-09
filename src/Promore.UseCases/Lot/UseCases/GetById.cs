using Promore.Core;
using Promore.Core.Contracts;
using Promore.Core.ViewModels.Responses;

namespace Promore.UseCases.Lot.UseCases;

public class GetById(ILotRepository lotRepository)
{
    public async Task<OperationResult<LotResponse>> Handle(string id)
    {
        var region = await lotRepository.GetByIdAsync(id);
        return OperationResult<LotResponse>.SuccessResult(region);
    }
}