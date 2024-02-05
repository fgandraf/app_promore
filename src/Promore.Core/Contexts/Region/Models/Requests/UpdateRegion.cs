using System.ComponentModel.DataAnnotations;

namespace Promore.Core.Contexts.Region.Models.Requests;

public class UpdateRegion
{
    [Required]
    public int Id { get; set; }
    
    [Required]
    public string Name { get; set; }
    
    public DateTime EstablishedDate { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    
}