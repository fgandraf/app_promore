using Promore.Core.Contexts.Region.Models.Requests;
using Promore.Core.Contexts.Region.Models.Responses;

namespace Promore.Core.Contexts.Region.Contracts;

public interface IRegionRepository
{
    Task<List<ReadRegion>> GetAll();
    Task<List<Entity.Region>> GetRegionsByIdListAsync(List<int> regionsId);
    Task<ReadRegion> GetByIdAsync(int id);
    Task<long> InsertAsync(CreateRegion model);
    Task<bool> UpdateAsync(UpdateRegion model);
    Task<bool> DeleteAsync(int id);
}