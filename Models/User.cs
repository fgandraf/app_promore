namespace PromoreApi.Models;

public class User
{
    public int Id { get; set; }
    public int Role { get; set; }
    public bool Active { get; set; }
    public string? UserName{ get; set; }
    public string? PasswordHash { get; set; }
}