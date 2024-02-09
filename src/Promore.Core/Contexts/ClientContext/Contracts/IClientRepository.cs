

namespace Promore.Core.Contexts.ClientContext.Contracts;

public interface IClientRepository
{
    Task<List<UseCases.GetAll.Response>> GetAll();
    Task<List<UseCases.GetAllByLotId.Response>> GetAllByLotId(string lotId);
    Task<UseCases.GetById.Response> GetByIdAsync(int id);
    Task<Entities.Client> GetClientByIdAsync(int id);
    Task<List<Entities.Client>> GetClientsByIdListAsync(List<int> clientsIds);
    Task<long> InsertAsync(Entities.Client client);
    Task<int> UpdateAsync(Entities.Client client);
    Task<int> DeleteAsync(int id);
}