using Promore.Api.Models.InputModels;
using Promore.Api.Models.ViewModels;

namespace Promore.Api.Repositories.Contracts;

public interface IClientRepository
{
    Task<List<ClientView>> GetAll();
    Task<ClientView> GetByIdAsync(int id);
    Task<long> InsertAsync(CreateClientInput model);
    Task<bool> UpdateAsync(UpdateClientInput model);
    Task<bool> DeleteAsync(int id);
}