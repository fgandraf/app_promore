using System.ComponentModel.DataAnnotations;

namespace Promore.Core.Contexts.RegionContext.UseCases.Create;

public class CreateRegionRequest
{
    [Required]
    public string Name { get; set; }
    
    public DateTime EstablishedDate { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    
    public List<int> Users { get; set; }
}