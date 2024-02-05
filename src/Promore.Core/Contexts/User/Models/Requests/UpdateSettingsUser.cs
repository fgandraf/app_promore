using System.ComponentModel.DataAnnotations;

namespace Promore.Core.Contexts.User.Models.Requests;

public class UpdateSettingsUser
{
    [Required]
    public int Id { get; set; }
    
    [Required]
    public bool Active { get; set; }
    
    [Required]
    [MinLength(1, ErrorMessage = "A lista deve conter pelo menos um elemento.")]
    public List<int> Roles { get; set; }
    
    [Required]
    [MinLength(1, ErrorMessage = "A lista deve conter pelo menos um elemento.")]
    public List<int> Regions { get; set; }
}