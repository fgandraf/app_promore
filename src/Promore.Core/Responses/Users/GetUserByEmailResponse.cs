
namespace Promore.Core.Responses.Users;

public record GetUserByEmailResponse(
    int Id,
    bool Active,
    string Email,
    string Name,
    string Cpf,
    string Profession,
    List<int> Roles,
    List<int> Regions,
    List<int> Lots
);