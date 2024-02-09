using Promore.Core.Contexts.RegionContext.Contracts;
using Promore.Core.Contexts.RegionContext.Entities;
using Promore.Core.Contexts.UserContext.Contracts;

namespace Promore.Core.Contexts.RegionContext.UseCases.Create;

public class Handler(IRegionRepository regionRepository, IUserRepository userRepository)
{
    public async Task<OperationResult<long>> Handle(CreateRegionRequest model)
    {
        var users = userRepository.GetEntitiesByIdsAsync(model.Users).Result;
        
        var region = new Region
        {
            Name = model.Name,
            EstablishedDate = model.EstablishedDate,
            StartDate = model.StartDate,
            EndDate = model.EndDate,
            Users = users
        };
        
        var id = await regionRepository.InsertAsync(region);

        return id > 0 ? OperationResult<long>.SuccessResult(id) : OperationResult<long>.FailureResult("Não foi possível inserir a região!");
    }
}