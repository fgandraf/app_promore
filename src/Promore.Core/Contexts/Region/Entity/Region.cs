namespace Promore.Core.Contexts.Region.Entity;

public class Region
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime EstablishedDate { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }


    public IList<User.Entity.User> Users { get; set; }
    public IList<Lot.Entity.Lot> Lots { get; set; }
}