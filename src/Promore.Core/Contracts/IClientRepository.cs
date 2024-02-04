using Promore.Core.Models.InputModels;
using Promore.Core.Models.ViewModels;

namespace Promore.Core.Contracts;

public interface IClientRepository
{
    Task<List<ClientView>> GetAll();
    Task<ClientView> GetByIdAsync(int id);
    Task<long> InsertAsync(CreateClientInput model);
    Task<bool> UpdateAsync(UpdateClientInput model);
    Task<bool> DeleteAsync(int id);
}