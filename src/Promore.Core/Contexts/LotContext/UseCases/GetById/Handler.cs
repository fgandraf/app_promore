using Promore.Core.Contexts.LotContext.Contracts;

namespace Promore.Core.Contexts.LotContext.UseCases.GetById;

public class Handler(ILotRepository lotRepository)
{
    public async Task<OperationResult<Response>> Handle(string id)
    {
        var region = await lotRepository.GetByIdAsync(id);
        return OperationResult<Response>.SuccessResult(region);
    }
}