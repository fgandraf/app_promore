namespace Promore.Core.ViewModels.Responses;

public record ClientResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Cpf { get; set; }
    public string Phone { get; set; }
    public string MothersName { get; set; }
    public DateTime BirthdayDate { get; set; }
    public string LotId { get; set; }
}