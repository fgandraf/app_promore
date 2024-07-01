namespace Promore.Core.Responses.Regions;

public record GetRegionByIdResponse(
    int Id,
    string Name,
    DateTime EstablishedDate,
    DateTime StartDate,
    DateTime EndDate
);
