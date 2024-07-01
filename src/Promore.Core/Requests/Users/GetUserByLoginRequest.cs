using System.ComponentModel.DataAnnotations;

namespace Promore.Core.Requests.Users;

public class GetUserByLoginRequest : Request
{
    [Required]
    [EmailAddress]
    public string Email{ get; set; }
    
    [Required]
    public string Password { get; set; }
}