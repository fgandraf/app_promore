using Promore.Core.Contexts.Region.Models.Responses;

namespace Promore.Core.Contexts.Region.Contracts;

public interface IRegionRepository
{
    Task<List<ReadRegion>> GetAll();
    Task<List<Entity.Region>> GetRegionsByIdListAsync(List<int> regionsId);
    Task<Entity.Region> GetRegionByIdAsync(int id);
    Task<ReadRegion> GetByIdAsync(int id);
    Task<long> InsertAsync(Entity.Region region);
    Task<int> UpdateAsync(Entity.Region region);
    Task<int> DeleteAsync(int id);
}