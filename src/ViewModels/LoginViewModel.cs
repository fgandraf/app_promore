namespace PromoreApi.ViewModels;

public record LoginViewModel
{
    public string Email{ get; set; }
    public string PasswordHash { get; set; }
}