using Promore.Core.Contexts.ClientContext.Contracts;
using Promore.Core.Contexts.LotContext.Contracts;
using Promore.Core.Contexts.LotContext.Entities;
using Promore.Core.Contexts.RegionContext.Contracts;
using Promore.Core.Contexts.UserContext.Contracts;

namespace Promore.Core.Contexts.LotContext.UseCases.Create;

public class Hander(ILotRepository lotRepository, IUserRepository userRepository, IRegionRepository regionRepository, IClientRepository clientRepository)
{
    public async Task<OperationResult<string>> Handle(CreateLotRequest model)
    {
        var lot = new Lot
        {
            Block = model.Block,
            Number = model.Number,
            SurveyDate = model.SurveyDate,
            LastModifiedDate = model.LastModifiedDate,
            Status = model.Status,
            Comments = model.Comments,
            User = userRepository.GetEntityByIdAsync(model.UserId).Result,
            Region = regionRepository.GetRegionByIdAsync(model.RegionId).Result,
            Clients = clientRepository.GetClientsByIdListAsync(model.Clients).Result
        };
        
        var id = await lotRepository.InsertAsync(lot);

        return string.IsNullOrEmpty(id) ? OperationResult<string>.SuccessResult(id) : OperationResult<string>.FailureResult("Não foi possível inserir o lote!");
    }
}