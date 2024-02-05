using System.ComponentModel.DataAnnotations;

namespace Promore.Core.Contexts.Lot.Models.Requests;

public class UpdateLot
{
    [Required]
    public string Id { get; set; }
    
    [Required]
    [MaxLength(2)]
    public string Block { get; set; }
    
    [Required]
    public int Number { get; set; }
    
    public DateTime SurveyDate { get; set; }
    public DateTime LastModifiedDate { get; set; }
    public int Status { get; set; }
    public string Comments { get; set; }
    
    [Required]
    public int UserId { get; set; }
    
    [Required]
    public int RegionId { get; set; }
    
    public List<int> Clients { get; set; }
}