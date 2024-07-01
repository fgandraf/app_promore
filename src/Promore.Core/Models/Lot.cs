namespace Promore.Core.Models;

public class Lot
{
    public int Id { get; set; }
    public string Block { get; set; } = string.Empty;
    public int Number { get; set; }
    public DateTime SurveyDate { get; set; }
    public DateTime LastModifiedDate { get; set; }
    public int Status { get; set; }
    public string Comments { get; set; } = string.Empty;
    public int? UserId { get; set; }
    public int RegionId { get; set; }

    public User User { get; set; } = null!;
    public Region Region { get; set; } = null!;
    public List<Client>? Clients { get; set; }
}