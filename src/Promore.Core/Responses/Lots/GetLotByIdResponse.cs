namespace Promore.Core.Responses.Lots;

public record GetLotByIdResponse (
     string Id,
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