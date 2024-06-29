using Promore.Core.Models;
using Promore.Core.Responses.Regions;

namespace Promore.Core.Handlers;

public interface IRegionHandler
{
    Task<List<GetRegionsResponse>> GetAll();
    Task<List<Region>> GetRegionsByIdListAsync(List<int> regionsId);
    Task<Region> GetRegionByIdAsync(int id);
    Task<GetRegionsByIdResponse> GetByIdAsync(int id);
    Task<long> InsertAsync(Region region);
    Task<int> UpdateAsync(Region region);
    Task<int> DeleteAsync(int id);
}