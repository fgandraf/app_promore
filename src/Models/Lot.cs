using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PromoreApi.Models;

[Table("Lot")]
public class Lot
{
    //[Key]
    [MaxLength(5)]
    public string Id { get; set; }
    
    [MaxLength(2)]
    public string Block { get; set; }
    
    public int Number { get; set; }
    public DateTime SurveyDate { get; set; }
    public DateTime LastModifiedDate { get; set; }
    public int Status { get; set; }
    
    [MaxLength(1000)]
    public string Comments { get; set; }
    
    public int ProfessionalId { get; set; }
    public int RegionId { get; set; }
}