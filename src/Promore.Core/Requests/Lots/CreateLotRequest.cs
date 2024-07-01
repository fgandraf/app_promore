using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Promore.Core.Requests.Lots;

public class CreateLotRequest : Request
{
    [Required(ErrorMessage = "O campo 'Id' é obrigatório.")]
    public int Id { get; set; }
    
    [Required(ErrorMessage = "O campo 'Block' é obrigatória.")]
    [MaxLength(2)]
    public string Block { get; set; }
    
    [Required(ErrorMessage = "O campo 'Number' é obrigatório.")]
    public int Number { get; set; }
    
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