using PromoreApi.Models;

namespace PromoreApi.ViewModels;

public class UserGetVO
{
    public int Id { get; set; }
    public bool Active { get; set; }
    public string Email{ get; set; }
    public List<int> Roles { get; set; }
    public List<int> Regions { get; set; }
}