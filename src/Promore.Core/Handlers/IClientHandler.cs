using Promore.Core.Models;
using Promore.Core.Requests.Clients;
using Promore.Core.Responses.Clients;

namespace Promore.Core.Handlers;

public interface IClientHandler
{
    Task<OperationResult<List<GetClientsResponse>>> GetAllAsync(GetAllClientsRequest request);
    Task<OperationResult<List<GetClientsByLotIdResponse>>> GetAllByLotIdAsync(GetAllClientsByLotIdRequest request);
    Task<OperationResult<GetClientByIdResponse>> GetClientByIdAsync(GetClientByIdRequest request);
    Task<OperationResult<long>> CreateAsync(CreateClientRequest request);
    Task<OperationResult> UpdateAsync(UpdateClientRequest request);
    Task<OperationResult> DeleteAsync(DeleteClientRequest request);
}