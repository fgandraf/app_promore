namespace Promore.Core.Contexts.RegionContext.Contracts;

public interface IRegionRepository
{
    Task<List<UseCases.GetAll.Response>> GetAll();
    Task<List<Entities.Region>> GetRegionsByIdListAsync(List<int> regionsId);
    Task<Entities.Region> GetRegionByIdAsync(int id);
    Task<UseCases.GetById.Response> GetByIdAsync(int id);
    Task<long> InsertAsync(Entities.Region region);
    Task<int> UpdateAsync(Entities.Region region);
    Task<int> DeleteAsync(int id);
}