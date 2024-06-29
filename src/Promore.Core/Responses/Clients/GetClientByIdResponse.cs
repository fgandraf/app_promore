namespace Promore.Core.Responses.Clients;

public record GetClientByIdResponse(
    int Id, 
    string Name, 
    string Cpf, 
    string Phone, 
    string MothersName, 
    DateTime BirthdayDate, 
    string LotId
    );