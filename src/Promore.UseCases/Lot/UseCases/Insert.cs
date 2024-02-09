using Promore.Core;
using Promore.Core.Contracts;
using Promore.Core.ViewModels.Requests;

namespace Promore.UseCases.Lot.UseCases;

public class Insert(ILotRepository lotRepository, IUserRepository userRepository, IRegionRepository regionRepository, IClientRepository clientRepository)
{
    public async Task<OperationResult<string>> Handle(LotCreateRequest model)
    {
        var lot = new Core.Entities.Lot
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