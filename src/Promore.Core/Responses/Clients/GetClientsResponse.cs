namespace Promore.Core.Responses.Clients;

public record GetClientsResponse(
    int Id, 
    string Name, 
    string Cpf, 
    string Phone, 
    string MothersName, 
    DateTime BirthdayDate, 
    int LotId
    );