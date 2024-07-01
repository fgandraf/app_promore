namespace Promore.Core.Responses.Lots;

public record LotsStatusResponse (
     int Id,
     int Status,
     int? UserId
     );
