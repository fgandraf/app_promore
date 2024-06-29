using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Promore.Core.Requests.Regions;

public class CreateRegionRequest
{
    [Required(ErrorMessage = "O campo 'Name' é obrigatório.")]
    [MaxLength(100)]
    [DefaultValue("")]
    public string Name { get; set; }
    
    public DateTime EstablishedDate { get; set; }
    
    public DateTime StartDate { get; set; }
    
    public DateTime EndDate { get; set; }
    
    [DefaultValue("[]")]
    public List<int> Users { get; set; }
}