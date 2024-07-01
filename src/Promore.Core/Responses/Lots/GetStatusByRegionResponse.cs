namespace Promore.Core.Responses.Lots;

public record GetStatusByRegionResponse (
     int Id,
     int Status,
     int? UserId
     );
