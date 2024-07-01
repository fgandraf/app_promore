namespace Promore.Core.Responses.Users;

public record UpdateUserSettingsResponse(
    int Id,
    bool Active,
    List<int> Roles,
    List<int> Regions);