using System.ComponentModel.DataAnnotations;

namespace PromoreApi.Models;

public class Client
{
    public int Id { get; set; }
    
    [MaxLength(100)]
    public string Name { get; set; }
    
    [MaxLength(11)]
    public string Cpf { get; set; }
    
    [MaxLength(11)]
    public string Phone { get; set; }
    
    [MaxLength(100)]
    public string MothersName { get; set; }
    
    public DateTime BirthdayDate { get; set; }
    public int LotId { get; set; }
}