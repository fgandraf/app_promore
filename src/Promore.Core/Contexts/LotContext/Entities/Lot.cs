using Promore.Core.Contexts.ClientContext.Entities;
using Promore.Core.Contexts.RegionContext.Entities;
using Promore.Core.Contexts.UserContext.Entities;

namespace Promore.Core.Contexts.LotContext.Entities;

public class Lot
{
    public string Id { get; set; }
    public string Block { get; set; }
    public int Number { get; set; }
    public DateTime SurveyDate { get; set; }
    public DateTime LastModifiedDate { get; set; }
    public int Status { get; set; }
    public string Comments { get; set; }
    public int? UserId { get; set; }
    public int RegionId { get; set; }
    
    public User User { get; set; }
    public Region Region { get; set; }
    public List<Client> Clients { get; set; }
}