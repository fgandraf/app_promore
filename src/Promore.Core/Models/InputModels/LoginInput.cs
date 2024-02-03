using System.ComponentModel.DataAnnotations;

namespace Promore.Core.Models.InputModels;

public record LoginInput
{
    [Required]
    [EmailAddress]
    public string Email{ get; set; }
    
    [Required]
    public string Password { get; set; }
}