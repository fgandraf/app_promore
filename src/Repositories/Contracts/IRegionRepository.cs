using PromoreApi.Models.InputModels;
using PromoreApi.Models.ViewModels;

namespace PromoreApi.Repositories.Contracts;

public interface IRegionRepository
{
    Task<List<RegionView>> GetAll();
    Task<RegionView> GetByIdAsync(int id);
    Task<long> InsertAsync(CreateRegionInput model);
    Task<bool> UpdateAsync(UpdateRegionInput model);
    Task<bool> DeleteAsync(int id);
}