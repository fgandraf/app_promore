namespace Promore.Core.Contexts.Region.Models.Responses;

public class ReadRegion
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime EstablishedDate { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}