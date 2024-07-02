using System.Text.Json.Serialization;

namespace Promore.Core.Models;

public class Client
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Cpf { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string MothersName { get; set; } = string.Empty;
    public DateTime BirthdayDate { get; set; }
    public int LotId { get; set; }

    [JsonIgnore]
    public Lot Lot { get; set; } = null!;
}