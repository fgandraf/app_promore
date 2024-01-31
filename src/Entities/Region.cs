using System.Text.Json.Serialization;

namespace PromoreApi.Entities;

public class Region
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime EstablishedDate { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    [JsonIgnore]
    public IList<User> Users { get; set; }
    
    [JsonIgnore]
    public IList<Lot> Lots { get; set; }
}