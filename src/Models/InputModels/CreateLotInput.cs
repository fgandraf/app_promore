using System.ComponentModel.DataAnnotations;

namespace PromoreApi.Models.InputModels;

public class CreateLotInput
{
    [Required]
    public string Id { get; set; }
    
    [Required]
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