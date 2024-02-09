using Promore.Core.ViewModels.Responses;

namespace Promore.Core.Contracts;

public interface ILotRepository
{
    Task<Entities.Lot> GetLotById(string id);
    Task<List<LotStatusResponse>> GetStatusByRegion(int regionId);
    Task<LotResponse> GetByIdAsync(string id);
    Task<string> InsertAsync(Entities.Lot lot);
    Task<int> UpdateAsync(Entities.Lot lot);
    Task<int> DeleteAsync(string id);
}