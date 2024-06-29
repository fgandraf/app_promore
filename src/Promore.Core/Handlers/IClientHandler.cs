using Promore.Core.Models;
using Promore.Core.Responses.Clients;

namespace Promore.Core.Handlers;

public interface IClientHandler
{
    Task<List<GetClientsResponse>> GetAll();
    Task<List<GetClientsByLotIdResponse>> GetAllByLotId(string lotId);
    Task<GetClientByIdResponse> GetByIdAsync(int id);
    Task<Client> GetClientByIdAsync(int id);
    Task<List<Client>> GetClientsByIdListAsync(List<int> clientsIds);
    Task<long> InsertAsync(Client client);
    Task<int> UpdateAsync(Client client);
    Task<int> DeleteAsync(int id);
}