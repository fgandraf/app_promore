using System.ComponentModel.DataAnnotations;

namespace PromoreApi.Models;

public class User
{
    public int Id { get; set; }
    public int Role { get; set; }
    public bool Active { get; set; }
    
    [MaxLength(25)]
    public string UserName{ get; set; }
    
    [MaxLength(255)]
    public string PasswordHash { get; set; }
}