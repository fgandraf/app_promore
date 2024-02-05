using System.ComponentModel.DataAnnotations;

namespace Promore.Core.Contexts.User.Models.Requests;

public class UpdateInfoUser
{
    [Required]
    public int Id { get; set; }
    
    [Required]
    [EmailAddress(ErrorMessage = "Email inválido.")]
    public string Email{ get; set; }
    
    [Required]
    public string Password { get; set; }
    
    [Required]
    public string Name { get; set; }
    
    [Required]
    public string Cpf { get; set; }
    
    [Required]
    public string Profession { get; set; }
}