namespace Promore.Core.Contexts.Lot.Entity;

public class Lot
{
    public string Id { get; set; }
    public string Block { get; set; }
    public int Number { get; set; }
    public DateTime SurveyDate { get; set; }
    public DateTime LastModifiedDate { get; set; }
    public int Status { get; set; }
    public string Comments { get; set; }
    public int UserId { get; set; }
    public int RegionId { get; set; }
    
    public User.Entity.User User { get; set; }
    public Region.Entity.Region Region { get; set; }
    public List<Client.Entity.Client> Clients { get; set; }
}