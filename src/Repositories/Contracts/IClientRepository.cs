namespace PromoreApi.Repositories.Contracts;

public interface IClientRepository
{
    Task<IEnumerable<dynamic>> GetAll();
    Task<dynamic> GetByIdAsync(int id);
}