using System.ComponentModel.DataAnnotations;

namespace Promore.Core.Models.InputModels;

public class CreateUserInput
{
    [Required]
    public bool Active { get; set; }
    
    [Required]
    [EmailAddress]
    public string Email{ get; set; }
    
    [Required]
    public string Password { get; set; }
    
    [Required]
    public string Name { get; set; }
    
    [Required]
    public string Cpf { get; set; }
    
    [Required]
    public string Profession { get; set; }
    
    [Required]
    [MinLength(1, ErrorMessage = "A lista deve conter pelo menos um elemento.")]
    public List<int> Roles { get; set; }
    
    [Required]
    [MinLength(1, ErrorMessage = "A lista deve conter pelo menos um elemento.")]
    public List<int> Regions { get; set; }
}