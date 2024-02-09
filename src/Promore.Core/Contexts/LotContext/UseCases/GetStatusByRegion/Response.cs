namespace Promore.Core.Contexts.LotContext.UseCases.GetStatusByRegion;

public record Response (
     string Id,
     int Status,
     int? UserId
     );
