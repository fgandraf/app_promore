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
    Task<Response<Client?>> GetByIdAsync(GetClientByIdRequest request);
    Task<Response<List<ClientResponse>?>> GetAllByLotIdAsync(GetAllClientsByLotIdRequest request);
    Task<Response<List<ClientResponse>?>> GetAllAsync(GetAllClientsRequest request);
}