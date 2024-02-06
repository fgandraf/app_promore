using Promore.Core.Contexts.Client.Models.Responses;

namespace Promore.Core.Contexts.Client.Contracts;

public interface IClientRepository
{
    Task<List<ReadClient>> GetAll();
    Task<List<ReadClient>> GetAllByLotId(string lotId);
    Task<ReadClient> GetByIdAsync(int id);
    Task<Entity.Client> GetClientByIdAsync(int id);
    Task<List<Entity.Client>> GetClientsByIdListAsync(List<int> clientsIds);
    Task<long> InsertAsync(Entity.Client client);
    Task<int> UpdateAsync(Entity.Client client);
    Task<int> DeleteAsync(int id);
}