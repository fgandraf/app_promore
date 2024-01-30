using PromoreApi.Models;

namespace PromoreApi.ViewModels;

public class UserGetVO
{
    public int Id { get; set; }
    public bool Active { get; set; }
    public string Email{ get; set; }
    public List<Role> Roles { get; set; }
    public List<RegionVO> Regions { get; set; }
}