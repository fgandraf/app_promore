using Promore.Core.Models;
using Promore.Core.Responses.Lots;

namespace Promore.Core.Handlers;

public interface ILotHandler
{
    Task<Lot> GetLotById(string id);
    Task<List<GetStatusByRegionResponse>> GetStatusByRegion(int regionId);
    Task<GetLotByIdResponse> GetByIdAsync(string id);
    Task<string> InsertAsync(Lot lot);
    Task<int> UpdateAsync(Lot lot);
    Task<int> DeleteAsync(string id);
}