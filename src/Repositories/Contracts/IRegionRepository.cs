using PromoreApi.Entities;

namespace PromoreApi.Repositories.Contracts;

public interface IRegionRepository
{
    Task<List<Region>> GetAll();
    Task<Region> GetByIdAsync(int id);
}