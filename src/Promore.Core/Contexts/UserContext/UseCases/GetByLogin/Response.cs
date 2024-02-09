
namespace Promore.Core.Contexts.UserContext.UseCases.GetByLogin;

public record Response(
    int Id,
    bool Active,
    string Email,
    string Name,
    string Cpf,
    string Profession,
    List<int> Roles,
    List<int> Regions,
    List<string> Lots
);