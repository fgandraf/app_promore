using System.ComponentModel.DataAnnotations;

namespace PromoreApi.Models.InputModels;

public class UpdateUserInput
{
    [Required]
    public int Id { get; set; }
    
    [Required]
    public bool Active { get; set; }
    
    [Required]
    [EmailAddress]
    public string Email{ get; set; }
    
    [Required]
    public string Password { get; set; }
    
    [Required]
    [MinLength(1, ErrorMessage = "A lista deve conter pelo menos um elemento.")]
    public List<int> Roles { get; set; }
    
    [Required]
    [MinLength(1, ErrorMessage = "A lista deve conter pelo menos um elemento.")]
    public List<int> Regions { get; set; }
}