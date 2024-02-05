using System.Text.Json.Serialization;

namespace Promore.Core.Contexts.Role.Entity;

public class Role
{
    public int Id { get; set; }
    public string Name { get; set; }

    [JsonIgnore]
    public List<User.Entity.User> Users { get; set; }
}