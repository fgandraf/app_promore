using Promore.Core.Models;
using Promore.Core.Requests.Clients;
using Promore.Core.Responses;
using Promore.Core.Responses.Clients;

namespace Promore.Core.Handlers;

public interface IClientHandler
{
    Task<Response<Client?>> CreateAsync(CreateClientRequest request);
    Task<Response<Client?>> UpdateAsync(UpdateClientRequest request);
    Task<Response<Client?>> DeleteAsync(DeleteClientRequest request);
    
    
    
    
    
    Task<Response<List<GetClientsResponse>>> GetAllAsync(GetAllClientsRequest request);
    Task<OperationResult<List<GetClientsByLotIdResponse>>> GetAllByLotIdAsync(GetAllClientsByLotIdRequest request);
    Task<OperationResult<GetClientByIdResponse>> GetClientByIdAsync(GetClientByIdRequest request);
}