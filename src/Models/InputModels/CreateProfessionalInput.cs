namespace PromoreApi.Models.InputModels;

public class CreateProfessionalInput
{
    public string Name { get; set; }
    public string Cpf { get; set; }
    public string Profession { get; set; }
    public int UserId { get; set; }
}