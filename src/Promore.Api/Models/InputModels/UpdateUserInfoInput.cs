using System.ComponentModel.DataAnnotations;

namespace Promore.Api.Models.InputModels;

public class UpdateUserInfoInput
{
    [Required]
    public int Id { get; set; }
    
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
}