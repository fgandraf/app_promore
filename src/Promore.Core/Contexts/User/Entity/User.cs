namespace Promore.Core.Contexts.User.Entity;

public class User
{
    public int Id { get; set; }
    public bool Active { get; set; }
    public string Email{ get; set; }
    public string PasswordHash { get; set; }
    public string Name { get; set; }
    public string Cpf { get; set; }
    public string Profession { get; set; }
    
    public List<Role.Entity.Role> Roles { get; set; }
    public List<Region.Entity.Region> Regions { get; set; }
    public List<Lot.Entity.Lot> Lots { get; set; }
}