using Promore.Core.ViewModels.Responses;

namespace Promore.Core.Contracts;

public interface IRegionRepository
{
    Task<List<RegionResponse>> GetAll();
    Task<List<Entities.Region>> GetRegionsByIdListAsync(List<int> regionsId);
    Task<Entities.Region> GetRegionByIdAsync(int id);
    Task<RegionResponse> GetByIdAsync(int id);
    Task<long> InsertAsync(Entities.Region region);
    Task<int> UpdateAsync(Entities.Region region);
    Task<int> DeleteAsync(int id);
}