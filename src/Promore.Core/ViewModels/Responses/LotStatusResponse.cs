namespace Promore.Core.ViewModels.Responses;

public record LotStatusResponse
{
    public string Id { get; set; }
    public int Status { get; set; }
    public int? UserId { get; set; }
}