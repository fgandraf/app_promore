namespace PromoreApi.Repositories.Contracts;

public interface ILotRepository
{
    Task<IEnumerable<dynamic>> GetAll();
    Task<dynamic> GetByIdAsync(string id);
}