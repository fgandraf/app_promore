using System.ComponentModel.DataAnnotations;

namespace PromoreApi.Models;

public class Region
{
    public int Id { get; set; }
    
    [MaxLength(100)]
    public string Name { get; set; }
    
    public DateTime EstablishedDate { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}