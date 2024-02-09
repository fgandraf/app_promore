using Promore.Core.Contexts.LotContext.UseCases.GetById;
using Promore.Core.Contexts.LotContext.UseCases.GetStatusByRegion;
using Response = Promore.Core.Contexts.LotContext.UseCases.GetStatusByRegion.Response;

namespace Promore.Core.Contexts.LotContext.Contracts;

public interface ILotRepository
{
    Task<Entities.Lot> GetLotById(string id);
    Task<List<Response>> GetStatusByRegion(int regionId);
    Task<UseCases.GetById.Response> GetByIdAsync(string id);
    Task<string> InsertAsync(Entities.Lot lot);
    Task<int> UpdateAsync(Entities.Lot lot);
    Task<int> DeleteAsync(string id);
}