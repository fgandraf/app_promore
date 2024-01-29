namespace PromoreApi.Models;

public class Lot
{
    public string Id { get; set; }
    public string Block { get; set; }
    public int Number { get; set; }
    public DateTime SurveyDate { get; set; }
    public DateTime LastModifiedDate { get; set; }
    public int Status { get; set; }
    public string Comments { get; set; }
    
    
    public Professional Professional { get; set; }
    public Region Region { get; set; }
    public List<Client> Clients { get; set; }
}