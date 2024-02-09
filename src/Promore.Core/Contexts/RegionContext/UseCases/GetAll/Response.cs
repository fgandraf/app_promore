namespace Promore.Core.Contexts.RegionContext.UseCases.GetAll;

public record Response(
    int Id,
    string Name,
    DateTime EstablishedDate,
    DateTime StartDate,
    DateTime EndDate
);
