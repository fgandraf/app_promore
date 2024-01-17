using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PromoreApi.Models;

[Table("Lot")]
public class Lot
{
    [Key]
    public int Id { get; set; }
    
    public int Block { get; set; }
    public int Number { get; set; }
    public DateTime SurveyDate { get; set; }
    public DateTime LastModifiedDate { get; set; }
    public int Status { get; set; }
    
    [MaxLength(1000)]
    public string Comments { get; set; }
    
    public int ProfessionalId { get; set; }
    public int RegionId { get; set; }
}