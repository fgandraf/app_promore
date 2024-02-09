namespace Promore.Core.Contexts.UserContext.Contracts;

public interface IUserRepository
{
    Task<Entities.User> GetUserByLogin(UseCases.GetByLogin.LoginRequest model);
    Task<List<UseCases.GetAll.Response>> GetAllAsync();
    Task<long> InsertAsync(Entities.User user);
    Task<UseCases.GetById.Response> GetByIdAsync(int id);
    Task<UseCases.GetByEmail.Response> GetByEmailAsync(string address);
    Task<int> UpdateAsync(Entities.User user);

    
    Task<Entities.User> GetEntityByIdAsync(int id);
    Task<List<Entities.User>> GetEntitiesByIdsAsync(List<int> ids);
}