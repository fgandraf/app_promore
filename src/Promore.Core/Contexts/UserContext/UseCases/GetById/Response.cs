
namespace Promore.Core.Contexts.UserContext.UseCases.GetById;

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