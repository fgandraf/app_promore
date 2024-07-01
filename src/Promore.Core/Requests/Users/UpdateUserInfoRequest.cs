using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Promore.Core.Requests.Users;

public class UpdateUserInfoRequest : Request
{
    [Required(ErrorMessage = "O campo 'Id' é obrigatório.")]
    [DefaultValue("")]
    public int Id { get; set; }
    
    [Required(ErrorMessage = "O campo 'Email' é obrigatório.")]
    [EmailAddress(ErrorMessage = "Email inválido.")]
    [MaxLength(50)]
    [DefaultValue("")]
    public string Email{ get; set; }
    
    [Required(ErrorMessage = "O campo 'Password' é obrigatório.")]
    [MaxLength(100)]
    [DefaultValue("")]
    public string Password { get; set; }
    
    [Required(ErrorMessage = "O campo 'Name' é obrigatório.")]
    [MaxLength(100)]
    [DefaultValue("")]
    public string Name { get; set; }
    
    [Required(ErrorMessage = "O campo 'Cpf' é obrigatório.")]
    [Length(11,11,ErrorMessage = "'Cpf' deve conter 11 números.")]
    [DefaultValue("")]
    public string Cpf { get; set; }
    
    [Required(ErrorMessage = "O campo 'Profession' é obrigatório.")]
    [MaxLength(50)]
    [DefaultValue("")]
    public string Profession { get; set; }
}