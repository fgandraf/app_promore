using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PromoreApi.Models;

[Table("Region")]
public class Region
{
    [Key]
    public int Id { get; set; }
    
    [MaxLength(100)]
    public string Name { get; set; }
    
    public DateTime EstablishedDate { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}