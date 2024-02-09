using Promore.Core;
using Promore.Core.Contracts;
using Promore.Core.ViewModels.Requests;

namespace Promore.UseCases.Client.UseCases;

public class Insert(IClientRepository clientRepository, ILotRepository lotRepository)
{
    
    public async Task<OperationResult<long>> Handle(CreateClientRequest model)
    {
        var lot = lotRepository.GetLotById(model.LotId).Result;
        
        var client = new Core.Entities.Client
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