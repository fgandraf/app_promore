using PromoreApi.Models.InputModels;
using PromoreApi.Models.ViewModels;

namespace PromoreApi.Repositories.Contracts;

public interface ILotRepository
{
    Task<List<LotView>> GetAll();
    Task<LotView> GetByIdAsync(string id);
    Task<string> InsertAsync(CreateLotInput model);
    Task<bool> UpdateAsync(UpdateLotInput model);
    Task<bool> DeleteAsync(string id);
}