namespace PromoreApi.Entities;

public class Professional
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Cpf { get; set; }
    public string Profession { get; set; }
    
    
    public User User { get; set; }
    public List<Lot> Lots { get; set; }
}