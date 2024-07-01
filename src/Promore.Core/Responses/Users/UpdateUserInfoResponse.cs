
namespace Promore.Core.Responses.Users;

public record UpdateUserInfoResponse(
    int Id,
    string Email,
    string Name,
    string Cpf,
    string Profession);