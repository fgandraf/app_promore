namespace Promore.Core.Services.Contracts;

public interface ITokenService
{
    string GenerateToken(Contexts.User.Entity.User user);
}