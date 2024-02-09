using Promore.Core.Contexts.ClientContext.Contracts;
using Promore.Core.Contexts.LotContext.Contracts;

namespace Promore.Core.Contexts.ClientContext;

public class ClientHandler(IClientRepository clientRepository, ILotRepository lotRepository)
{
    public async Task<OperationResult> DeleteAsync(int id) 
        => await new UseCases.Delete.Handler(clientRepository).Handle(id);
    
    public async Task<OperationResult<List<UseCases.GetAll.Response>>> GetAllAsync() 
        => await new UseCases.GetAll.Handler(clientRepository).Handle();
    
    public async Task<OperationResult<List<UseCases.GetAllByLotId.Response>>> GetAllByLotIdAsync(string id) 
        => await new UseCases.GetAllByLotId.Handler(clientRepository).Handle(id);
    
    public async Task<OperationResult<UseCases.GetById.Response>> GetByIdAsync(int id) 
        => await new UseCases.GetById.Handler(clientRepository).Handle(id);
    
    public async Task<OperationResult<long>> CreateAsync(UseCases.Create.CreateClientRequest model) 
        => await new UseCases.Create.Handler(clientRepository, lotRepository).Handle(model);
    
    public async Task<OperationResult> UpdateAsync(UseCases.Update.UpdateClientRequest model) 
        => await new UseCases.Update.UpdateHandler(clientRepository, lotRepository).Handle(model);
}