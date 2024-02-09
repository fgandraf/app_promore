using Promore.Core.Contexts.ClientContext.Contracts;
using Promore.Core.Contexts.ClientContext.Entities;
using Promore.Core.Contexts.LotContext.Contracts;

namespace Promore.Core.Contexts.ClientContext.UseCases.Create;

public class Handler(IClientRepository clientRepository, ILotRepository lotRepository)
{
    
    public async Task<OperationResult<long>> Handle(CreateClientRequest model)
    {
        var lot = lotRepository.GetLotById(model.LotId).Result;
        
        var client = new Client
        {
            Name = model.Name,
            Cpf = model.Cpf,
            Phone = model.Phone,
            MothersName = model.MothersName,
            BirthdayDate = model.BirthdayDate,
            Lot = lot
        };
    
        var id = await clientRepository.InsertAsync(client);
    
        return id > 0 ? OperationResult<long>.SuccessResult(id) : OperationResult<long>.FailureResult("Não foi possível inserir o cliente!");
    }
}