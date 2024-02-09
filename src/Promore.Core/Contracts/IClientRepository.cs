using Promore.Core.ViewModels.Responses;

namespace Promore.Core.Contracts;

public interface IClientRepository
{
    Task<List<ClientResponse>> GetAll();
    Task<List<ClientResponse>> GetAllByLotId(string lotId);
    Task<ClientResponse> GetByIdAsync(int id);
    Task<Entities.Client> GetClientByIdAsync(int id);
    Task<List<Entities.Client>> GetClientsByIdListAsync(List<int> clientsIds);
    Task<long> InsertAsync(Entities.Client client);
    Task<int> UpdateAsync(Entities.Client client);
    Task<int> DeleteAsync(int id);
}