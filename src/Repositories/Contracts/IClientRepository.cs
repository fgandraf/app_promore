using PromoreApi.Models.InputModels;
using PromoreApi.Models.ViewModels;

namespace PromoreApi.Repositories.Contracts;

public interface IClientRepository
{
    Task<List<ClientView>> GetAll();
    Task<ClientView> GetByIdAsync(int id);
    Task<long> InsertAsync(CreateClientInput model);
    Task<bool> UpdateAsync(UpdateClientInput model);
    Task<bool> DeleteAsync(int id);
}