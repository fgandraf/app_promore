namespace PromoreApi.ViewModels;

public record LoginVO
{
    public string Email{ get; set; }
    public string PasswordHash { get; set; }
}