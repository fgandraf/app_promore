using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Promore.Core.Requests.Clients;

public class CreateClientRequest : Request
{
    [Required(ErrorMessage = "O campo 'Name' é obrigatório.")]
    [MaxLength(100, ErrorMessage = "O campo 'Name' deve conter até 100 caractéres")]
    [DefaultValue("")]
    public string Name { get; set; }
    
    [Required(ErrorMessage = "O campo 'Cpf' é obrigatório.")]
    [Length(11,11,ErrorMessage = "O campo 'Cpf' deve conter 11 números.")]
    [DefaultValue("")]
    public string Cpf { get; set; }
    
    [Required(ErrorMessage = "O campo 'Phone' é obrigatório.")]
    [Length(11,11,ErrorMessage = "O campo 'Phone' deve conter 11 números.")]
    [DefaultValue("")]
    public string Phone { get; set; }
    
    [MaxLength(100, ErrorMessage = "O campo 'MothersName' deve conter até 100 caractéres")]
    [DefaultValue("")]
    public string MothersName { get; set; }
    
    public DateTime BirthdayDate { get; set; }
    
    [Required(ErrorMessage = "O campo 'LotId' é obrigatório.")]
    public int LotId { get; set; }
}