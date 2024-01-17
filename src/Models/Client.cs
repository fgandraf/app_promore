using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PromoreApi.Models;

[Table("Client")]
public class Client
{
    [Key]
    public int Id { get; set; }
    
    [MaxLength(100)]
    public string Name { get; set; }
    
    [MaxLength(11)]
    public string Cpf { get; set; }
    
    [MaxLength(11)]
    public string Phone { get; set; }
    
    [MaxLength(100)]
    public string MothersName { get; set; }
    
    public DateTime BirthdayDate { get; set; }
    
    [MaxLength(5)]
    public string LotId { get; set; }
}