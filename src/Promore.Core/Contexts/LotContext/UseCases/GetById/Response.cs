namespace Promore.Core.Contexts.LotContext.UseCases.GetById;

public record Response (
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