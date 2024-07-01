namespace Promore.Core.Responses.Lots;

public record GetLotByIdResponse (
     int Id,
     string Block,
     int Number,
     DateTime SurveyDate,
     DateTime LastModifiedDate,
     int Status,
     string Comments,
     int? UserId,
     int RegionId,
     List<int> Clients
     );