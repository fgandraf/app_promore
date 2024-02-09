namespace Promore.Core.Contexts.ClientContext.UseCases.GetAll;

public record Response(
    int Id, 
    string Name, 
    string Cpf, 
    string Phone, 
    string MothersName, 
    DateTime BirthdayDate, 
    string LotId
    );