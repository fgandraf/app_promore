using Promore.Core.Models;
using Promore.Core.Requests.Lots;
using Promore.Core.Responses;
using Promore.Core.Responses.Lots;

namespace Promore.Core.Handlers;

public interface ILotHandler
{
    Task<Response<Lot?>> CreateAsync(CreateLotRequest request);
    Task<Response<Lot?>> UpdateAsync(UpdateLotRequest request);
    Task<Response<Lot?>> DeleteAsync(DeleteLotRequest request);
    Task<Response<Lot?>> GetByIdAsync(GetLotByIdRequest request);
    Task<PagedResponse<List<LotsStatusResponse>>> GetAllStatusByRegionIdAsync(GetLotsStatusByRegionIdRequest request);
}