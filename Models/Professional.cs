namespace PromoreApi.Models;

public class Professional
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Cpf { get; set; }
    public string? Profession { get; set; }
    public int UserId { get; set; }
}