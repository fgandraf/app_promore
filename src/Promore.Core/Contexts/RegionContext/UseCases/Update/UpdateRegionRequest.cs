using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Promore.Core.Contexts.RegionContext.UseCases.Update;

public class UpdateRegionRequest
{
    [Required(ErrorMessage = "O campo 'Id' é obrigatório.")]
    [DefaultValue("")]
    public int Id { get; set; }
    
    [Required(ErrorMessage = "O campo 'Name' é obrigatório.")]
    [MaxLength(100)]
    [DefaultValue("")]
    public string Name { get; set; }
    
    public DateTime EstablishedDate { get; set; }
    
    public DateTime StartDate { get; set; }
    
    public DateTime EndDate { get; set; }
    
}