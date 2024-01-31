namespace PromoreApi.Models.InputModels;

public class CreateRegionInput
{
    public string Name { get; set; }
    public DateTime EstablishedDate { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    
    public IList<int> Users { get; set; }
}