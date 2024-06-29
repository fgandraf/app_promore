namespace Promore.Core.Responses.Lots;

public record GetStatusByRegionResponse (
     string Id,
     int Status,
     int? UserId
     );
