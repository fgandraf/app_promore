namespace Promore.Core.Responses.Regions;

public record GetRegionsResponse(
    int Id,
    string Name,
    DateTime EstablishedDate,
    DateTime StartDate,
    DateTime EndDate
);
