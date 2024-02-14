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
            Id = model.Id,
            Block = new string(model.Id.Where(char.IsLetter).ToArray()),
            Number = Convert.ToInt32(new string(model.Id.Where(char.IsDigit).ToArray())),
            SurveyDate = model.SurveyDate,
            LastModifiedDate = model.LastModifiedDate,
            Status = model.Status,
            Comments = model.Comments,
            User = await userRepository.GetUserByIdAsync(model.UserId),
            Region = await regionRepository.GetRegionByIdAsync(model.RegionId)
        };
        
        var id = await lotRepository.InsertAsync(lot);

        return !string.IsNullOrEmpty(id) ? OperationResult<string>.SuccessResult(id) : OperationResult<string>.FailureResult("Não foi possível inserir o lote!");
    }
}