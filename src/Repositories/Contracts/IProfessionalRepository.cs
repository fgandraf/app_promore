using PromoreApi.Models.InputModels;
using PromoreApi.Models.ViewModels;

namespace PromoreApi.Repositories.Contracts;

public interface IProfessionalRepository
{
    Task<List<ProfessionalView>> GetAll();
    Task<ProfessionalView> GetByIdAsync(int id);
    Task<long> InsertAsync(CreateProfessionalInput model);
    Task<bool> UpdateAsync(UpdateProfessionalInput model);
    Task<bool> DeleteAsync(int id);
}