using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PromoreApi.Models;

[Table("User")]
public class User
{
    [Key]
    public int Id { get; set; }
    
    public int Role { get; set; }
    public bool Active { get; set; }
    
    [MaxLength(25)]   
    public string UserName{ get; set; }
    
    [MaxLength(255)]
    public string PasswordHash { get; set; }
}