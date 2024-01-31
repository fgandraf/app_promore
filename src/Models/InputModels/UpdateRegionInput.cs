namespace PromoreApi.Models.InputModels;

public class UpdateRegionInput
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime EstablishedDate { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}