using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Promore.Core.Requests.Users;

public class UpdateUserSettingsRequest
{
    [Required(ErrorMessage = "O campo 'Id' é obrigatório.")]
    [DefaultValue("")]
    public int Id { get; set; }
    
    [Required(ErrorMessage = "O campo 'Active' é obrigatório.")]
    [DefaultValue("")]
    public bool Active { get; set; }
    
    [Required(ErrorMessage = "O campo 'Roles' é obrigatório.")]
    [MinLength(1, ErrorMessage = "A lista deve conter pelo menos um elemento.")]
    [DefaultValue("[]")]
    public List<int> Roles { get; set; }
    
    [Required(ErrorMessage = "O campo 'Regions' é obrigatório.")]
    [MinLength(1, ErrorMessage = "A lista deve conter pelo menos um elemento.")]
    [DefaultValue("[]")]
    public List<int> Regions { get; set; }
}