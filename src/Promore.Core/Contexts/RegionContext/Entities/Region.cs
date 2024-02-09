using Promore.Core.Contexts.LotContext.Entities;
using Promore.Core.Contexts.UserContext.Entities;

namespace Promore.Core.Contexts.RegionContext.Entities;

public class Region
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime EstablishedDate { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }


    public IList<User> Users { get; set; }
    public IList<Lot> Lots { get; set; }
}