using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Promore.Core.Contexts.ClientContext.UseCases.Update;

public class UpdateClientRequest
{
    [Required(ErrorMessage = "O campo 'Id' é obrigatório.")]
    [DefaultValue("")]
    public int Id { get; set; }
    
    [Required(ErrorMessage = "O campo 'Name' é obrigatório.")]
    [MaxLength(100)]
    [DefaultValue("")]
    public string Name { get; set; }
    
    [Required(ErrorMessage = "O campo 'Cpf' é obrigatório.")]
    [Length(11,11,ErrorMessage = "'Cpf' deve conter 11 números.")]
    [DefaultValue("")]
    public string Cpf { get; set; }
    
    [Required(ErrorMessage = "O campo 'Phone' é obrigatório.")]
    [Length(11,11,ErrorMessage = "'Phone' deve conter 11 números.")]
    [DefaultValue("")]
    public string Phone { get; set; }
    
    [MaxLength(100)]
    public string MothersName { get; set; }
    
    public DateTime BirthdayDate { get; set; }
    
    [Required(ErrorMessage = "O campo 'LotId' é obrigatório.")]
    [MaxLength(5)]
    [DefaultValue("")]
    public string LotId { get; set; }
}