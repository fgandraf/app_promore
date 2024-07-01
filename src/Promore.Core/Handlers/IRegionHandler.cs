using Promore.Core.Requests.Regions;
using Promore.Core.Responses.Regions;

namespace Promore.Core.Handlers;

public interface IRegionHandler
{
    Task<OperationResult<List<GetRegionsResponse>>> GetAllAsync(GetAllRegionsRequest request);
    Task<OperationResult<GetRegionsByIdResponse>> GetByIdAsync(GetRegionByIdRequest request);
    Task<OperationResult<long>> CreateAsync(CreateRegionRequest request);
    Task<OperationResult> UpdateAsync(UpdateRegionRequest request);
    Task<OperationResult> DeleteAsync(DeleteRegionRequest request);
}