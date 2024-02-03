using System.ComponentModel.DataAnnotations;

namespace Promore.Api.Models.InputModels;

public class UpdateClientInput
{
    [Required]
    public int Id { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string Name { get; set; }
    
    [MaxLength(11)]
    public string Cpf { get; set; }
    
    [MaxLength(11)]
    public string Phone { get; set; }
    
    [MaxLength(100)]
    public string MothersName { get; set; }
    
    public DateTime BirthdayDate { get; set; }
    
    [Required]
    [MaxLength(5)]
    public string LotId { get; set; }
}