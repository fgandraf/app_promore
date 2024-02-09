using System.ComponentModel.DataAnnotations;

namespace Promore.Core.ViewModels.Requests;

public class RegionCreateRequest
{
    [Required]
    public string Name { get; set; }
    
    public DateTime EstablishedDate { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    
    public List<int> Users { get; set; }
}