
namespace Promore.Core.Entities;

public class User
{
    public int Id { get; set; }
    public bool Active { get; set; }
    public string Email{ get; set; }
    public string PasswordHash { get; set; }
    public string Name { get; set; }
    public string Cpf { get; set; }
    public string Profession { get; set; }
    
    public List<Role> Roles { get; set; }
    public List<Region> Regions { get; set; }
    public List<Lot> Lots { get; set; }
}