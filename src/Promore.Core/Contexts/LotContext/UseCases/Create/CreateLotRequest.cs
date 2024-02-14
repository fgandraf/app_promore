using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Promore.Core.Contexts.LotContext.UseCases.Create;

public class CreateLotRequest
{
    [Required(ErrorMessage = "O campo 'Id' é obrigatório.")]
    [MaxLength(5)]
    [DefaultValue("")]
    public string Id { get; set; }
    
    public DateTime SurveyDate { get; set; }
    
    public DateTime LastModifiedDate { get; set; }
    
    [DefaultValue("")]
    public int Status { get; set; }
    
    [DefaultValue("")]
    public string Comments { get; set; }
    
    [Required(ErrorMessage = "O campo 'UserId' é obrigatório.")]
    [DefaultValue(0)]
    public int UserId { get; set; }
    
    [Required(ErrorMessage = "O campo 'RegionId' é obrigatório.")]
    [DefaultValue(0)]
    public int RegionId { get; set; }
    
}