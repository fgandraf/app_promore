namespace PromoreApi.Models;

public class User
{
    public int Id { get; set; }
    public bool Active { get; set; }
    public string Email{ get; set; }
    public string PasswordHash { get; set; }


    public List<Role> Roles { get; set; }
    public List<Region> Regions { get; set; }
}