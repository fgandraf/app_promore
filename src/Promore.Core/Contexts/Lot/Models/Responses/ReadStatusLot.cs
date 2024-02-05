namespace Promore.Core.Contexts.Lot.Models.Responses;

public record ReadStatusLot
{
    public string Id { get; set; }
    public int Status { get; set; }
    public int UserId { get; set; }
}