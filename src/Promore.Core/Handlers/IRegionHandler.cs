using Promore.Core.Models;
using Promore.Core.Requests.Regions;
using Promore.Core.Responses;
using Promore.Core.Responses.Regions;

namespace Promore.Core.Handlers;

public interface IRegionHandler
{
    Task<Response<Region?>> CreateAsync(CreateRegionRequest request);
    Task<Response<Region?>> UpdateAsync(UpdateRegionRequest request);
    Task<Response<Region?>> DeleteAsync(DeleteRegionRequest request);
    
    
    
    
    Task<OperationResult<List<GetRegionsResponse>>> GetAllAsync(GetAllRegionsRequest request);
    Task<OperationResult<GetRegionsByIdResponse>> GetByIdAsync(GetRegionByIdRequest request);
}