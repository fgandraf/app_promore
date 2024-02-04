using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Promore.Core.Models.InputModels;

public class CreateUserInput
{
    [Required(ErrorMessage = "O campo 'Active' é obrigatório.")]
    [DefaultValue(false)]
    public bool Active { get; set; }

    [Required(ErrorMessage = "O campo 'Email' é obrigatório.")]
    [EmailAddress(ErrorMessage = "Email inválido.")]
    [DefaultValue("")]
    public string Email { get; set; }
    
    [Required(ErrorMessage = "O campo 'Password' é obrigatório.")]
    [DefaultValue("")]
    public string Password { get; set; }

    [Required(ErrorMessage = "O campo 'Name' é obrigatório.")]
    [DefaultValue("")]
    public string Name { get; set; }
    
    [Required(ErrorMessage = "O campo 'Cpf' é obrigatório.")]
    [Length(11,11,ErrorMessage = "Cpf deve conter 11 números.")]
    [DefaultValue("")]
    public string Cpf { get; set; } 
    
    [Required(ErrorMessage = "O campo 'Profession' é obrigatório.")]
    [DefaultValue("")]
    public string Profession { get; set; }

    [Required]
    [MinLength(1, ErrorMessage = "A lista deve conter pelo menos um elemento.")]
    [DefaultValue("[]")]
    public List<int> Roles { get; set; }
    
    [Required]
    [MinLength(1, ErrorMessage = "A lista deve conter pelo menos um elemento.")]
    [DefaultValue("[]")]
    public List<int> Regions { get; set; }
}