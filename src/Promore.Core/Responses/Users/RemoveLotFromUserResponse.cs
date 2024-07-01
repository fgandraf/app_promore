namespace Promore.Core.Responses.Users;

public record RemoveLotFromUserResponse(
    int Id,
    List<int> LotsId
);