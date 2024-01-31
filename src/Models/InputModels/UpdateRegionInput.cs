using System.ComponentModel.DataAnnotations;

namespace PromoreApi.Models.InputModels;

public class UpdateRegionInput
{
    [Required]
    public int Id { get; set; }
    
    [Required]
    public string Name { get; set; }
    
    public DateTime EstablishedDate { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}