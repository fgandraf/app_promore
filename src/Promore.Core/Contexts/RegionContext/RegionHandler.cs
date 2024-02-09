using Promore.Core.Contexts.RegionContext.Contracts;
using Promore.Core.Contexts.UserContext.Contracts;

namespace Promore.Core.Contexts.RegionContext;

public class RegionHandler(IRegionRepository regionRepository, IUserRepository userRepository)
{
    public async Task<OperationResult<List<UseCases.GetAll.Response>>> GetAllAsync()
        => await new UseCases.GetAll.Handler(regionRepository).Handle();

    public async Task<OperationResult<UseCases.GetById.Response>> GetByIdAsync(int id)
        => await new UseCases.GetById.Handler(regionRepository).Handle(id);
    
    public async Task<OperationResult<long>> CreateAsync(UseCases.Create.CreateRegionRequest model)
        => await new UseCases.Create.Handler(regionRepository, userRepository).Handle(model);
    
    public async Task<OperationResult> UpdateAsync(UseCases.Update.UpdateRegionRequest model)
        => await new UseCases.Update.Handler(regionRepository).Handle(model);
    
    public async Task<OperationResult> DeleteAsync(int id)
        => await new UseCases.Delete.Handler(regionRepository).Handle(id);
}