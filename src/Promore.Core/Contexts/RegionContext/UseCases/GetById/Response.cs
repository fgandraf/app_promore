namespace Promore.Core.Contexts.RegionContext.UseCases.GetById;

public record Response(
    int Id,
    string Name,
    DateTime EstablishedDate,
    DateTime StartDate,
    DateTime EndDate
);
