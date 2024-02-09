using System.ComponentModel.DataAnnotations;

namespace Promore.Core.Contexts.UserContext.UseCases.GetByLogin;

public record LoginRequest
{
    [Required]
    [EmailAddress]
    public string Email{ get; set; }
    
    [Required]
    public string Password { get; set; }
}