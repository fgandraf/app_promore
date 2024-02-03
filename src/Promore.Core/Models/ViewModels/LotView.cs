namespace Promore.Core.Models.ViewModels;

public class LotView
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
    public List<int> Clients { get; set; }
}