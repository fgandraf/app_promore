namespace Promore.Core.Responses.Regions;

public record GetRegionsByIdResponse(
    int Id,
    string Name,
    DateTime EstablishedDate,
    DateTime StartDate,
    DateTime EndDate
);
