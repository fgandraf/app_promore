using Promore.Core.Requests.Lots;
using Promore.Core.Responses.Lots;

namespace Promore.Core.Handlers;

public interface ILotHandler
{
    Task<OperationResult<GetLotByIdResponse>> GetByIdAsync(GetLotByIdRequest request);
    Task<OperationResult<List<GetStatusByRegionResponse>>> GetStatusByRegionAsync(GetLotsStatusByRegionIdRequest request);
    Task<OperationResult<int>> CreateAsync(CreateLotRequest request);
    Task<OperationResult> UpdateAsync(UpdateLotRequest request);
    Task<OperationResult> DeleteAsync(DeleteLotRequest request);
}