using System.ComponentModel.DataAnnotations;

namespace Promore.Core.ViewModels.Requests;

public class RegionUpdateRequest
{
    [Required]
    public int Id { get; set; }
    
    [Required]
    public string Name { get; set; }
    
    public DateTime EstablishedDate { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    
}