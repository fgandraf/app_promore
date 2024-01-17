using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PromoreApi.Models;

[Table("Professional")]
public class Professional
{
    [Key]
    public int Id { get; set; }
    
    [MaxLength(100)]
    public string Name { get; set; }
    
    [MaxLength(11)]
    public string Cpf { get; set; }
    
    [MaxLength(50)]
    public string Profession { get; set; }
    
    public int UserId { get; set; }
}