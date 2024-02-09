using Promore.Core;
using Promore.Core.Contracts;
using Promore.Core.ViewModels.Requests;
using Promore.Core.ViewModels.Responses;

namespace Promore.UseCases.Region;

public class RegionHandler(IRegionRepository regionRepository, IUserRepository userRepository)
{
    public async Task<OperationResult<List<RegionResponse>>> GetAllAsync()
        => await new UseCases.GetAll(regionRepository).Handle();

    public async Task<OperationResult<RegionResponse>> GetByIdAsync(int id)
        => await new UseCases.GetById(regionRepository).Handle(id);
    
    public async Task<OperationResult<long>> InsertAsync(RegionCreateRequest model)
        => await new UseCases.Insert(regionRepository, userRepository).Handle(model);
    
    public async Task<OperationResult> UpdateAsync(RegionUpdateRequest model)
        => await new UseCases.Update(regionRepository).Handle(model);
    
    public async Task<OperationResult> DeleteAsync(int id)
        => await new UseCases.Delete(regionRepository).Handle(id);
}