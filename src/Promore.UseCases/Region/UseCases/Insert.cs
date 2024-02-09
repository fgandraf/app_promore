using Promore.Core;
using Promore.Core.Contracts;
using Promore.Core.ViewModels.Requests;

namespace Promore.UseCases.Region.UseCases;

public class Insert(IRegionRepository regionRepository, IUserRepository userRepository)
{
    public async Task<OperationResult<long>> Handle(RegionCreateRequest model)
    {
        var users = userRepository.GetEntitiesByIdsAsync(model.Users).Result;
        
        var region = new Core.Entities.Region
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