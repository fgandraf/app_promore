using System.ComponentModel.DataAnnotations;

namespace Promore.Core.Contexts.User.Models.Requests;

public record Login
{
    [Required]
    [EmailAddress]
    public string Email{ get; set; }
    
    [Required]
    public string Password { get; set; }
}