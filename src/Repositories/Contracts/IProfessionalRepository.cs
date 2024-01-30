namespace PromoreApi.Repositories.Contracts;

public interface IProfessionalRepository
{
    Task<IEnumerable<dynamic>> GetAll();
    Task<dynamic> GetByIdAsync(int id);
}