using Promore.Core.Contexts.Client.Models.Requests;
using Promore.Core.Contexts.Client.Models.Responses;

namespace Promore.Core.Contexts.Client.Contracts;

public interface IClientRepository
{
    Task<List<ReadClient>> GetAll();
    Task<ReadClient> GetByIdAsync(int id);
    Task<long> InsertAsync(CreateClient model);
    Task<bool> UpdateAsync(UpdateClient model);
    Task<bool> DeleteAsync(int id);
}