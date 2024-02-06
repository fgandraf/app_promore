using Promore.Core.Contexts.Lot.Models.Responses;

namespace Promore.Core.Contexts.Lot.Contracts;

public interface ILotRepository
{
    Task<Entity.Lot> GetLotById(string id);
    Task<List<ReadStatusLot>> GetStatusByRegion(int regionId);
    Task<ReadLot> GetByIdAsync(string id);
    Task<string> InsertAsync(Entity.Lot lot);
    Task<int> UpdateAsync(Entity.Lot lot);
    Task<int> DeleteAsync(string id);
}