using Promore.Core.Contexts.Lot.Models.Requests;
using Promore.Core.Contexts.Lot.Models.Responses;

namespace Promore.Core.Contexts.Lot.Contracts;

public interface ILotRepository
{
    Task<List<ReadLot>> GetAll();
    Task<List<ReadStatusLot>> GetStatusByRegion(int regionId);
    Task<ReadLot> GetByIdAsync(string id);
    Task<string> InsertAsync(CreateLot model);
    Task<bool> UpdateAsync(UpdateLot model);
    Task<bool> DeleteAsync(string id);
}