using Promore.Core;
using Promore.Core.Contracts;
using Promore.Core.ViewModels.Requests;
using Promore.Core.ViewModels.Responses;

namespace Promore.UseCases.Client;

public class ClientHandler(IClientRepository clientRepository, ILotRepository lotRepository)
{
    public async Task<OperationResult> DeleteAsync(int id) 
        => await new UseCases.Delete(clientRepository).Handle(id);
    
    public async Task<OperationResult<List<ClientResponse>>> GetAllAsync() 
        => await new UseCases.GetAll(clientRepository).Handle();
    
    public async Task<OperationResult<List<ClientResponse>>> GetAllByLotIdAsync(string id) 
        => await new UseCases.GetAllByLotId(clientRepository).Handle(id);
    
    public async Task<OperationResult<ClientResponse>> GetByIdAsync(int id) 
        => await new UseCases.GetById(clientRepository).Handle(id);
    
    public async Task<OperationResult<long>> InsertAsync(CreateClientRequest model) 
        => await new UseCases.Insert(clientRepository, lotRepository).Handle(model);
    
    public async Task<OperationResult> UpdateAsync(UpdateClientRequest model) 
        => await new UseCases.Update(clientRepository, lotRepository).Handle(model);
}