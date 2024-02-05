using System.ComponentModel.DataAnnotations;

namespace Promore.Core.Contexts.Region.Models.Requests;

public class CreateRegion
{
    [Required]
    public string Name { get; set; }
    
    public DateTime EstablishedDate { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    
    public IList<int> Users { get; set; }
}