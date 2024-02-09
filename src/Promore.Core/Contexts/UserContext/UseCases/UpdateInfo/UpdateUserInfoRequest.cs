using System.ComponentModel.DataAnnotations;

namespace Promore.Core.Contexts.UserContext.UseCases.UpdateInfo;

public class UpdateUserInfoRequest
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