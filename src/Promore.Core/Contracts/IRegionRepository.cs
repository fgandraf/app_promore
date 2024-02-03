using Promore.Core.Models.InputModels;
using Promore.Core.Models.ViewModels;

namespace Promore.Core.Repositories.Contracts;

public interface IRegionRepository
{
    Task<List<RegionView>> GetAll();
    Task<RegionView> GetByIdAsync(int id);
    Task<long> InsertAsync(CreateRegionInput model);
    Task<bool> UpdateAsync(UpdateRegionInput model);
    Task<bool> DeleteAsync(int id);
}