using Promore.Api.Models.InputModels;
using Promore.Api.Models.ViewModels;

namespace Promore.Api.Repositories.Contracts;

public interface IRegionRepository
{
    Task<List<RegionView>> GetAll();
    Task<RegionView> GetByIdAsync(int id);
    Task<long> InsertAsync(CreateRegionInput model);
    Task<bool> UpdateAsync(UpdateRegionInput model);
    Task<bool> DeleteAsync(int id);
}