using Promore.Core.Models.InputModels;
using Promore.Core.Models.ViewModels;

namespace Promore.Core.Repositories.Contracts;

public interface ILotRepository
{
    Task<List<LotView>> GetAll();
    Task<List<LotStatusView>> GetStatusByRegion(int regionId);
    Task<LotView> GetByIdAsync(string id);
    Task<string> InsertAsync(CreateLotInput model);
    Task<bool> UpdateAsync(UpdateLotInput model);
    Task<bool> DeleteAsync(string id);
}