using PromoreApi.Data;
using PromoreApi.Models;

namespace PromoreApi;

public static class DbInserts
{
    private const string ConnectionString = "Server=localhost,1433;Database=Promore;User ID=sa;Password=1q2w3e4r@#$;Encrypt=false";

    public static void InsertData()
    {
        InsertUsers();
        InsertProfessionals();
        InsertRegion();
        InsertLots();
        InsertClients();
    }
    
    private static void InsertUsers()
    {
        using var context = new PromoreDataContext(ConnectionString);
        
        if (context.Users.Any())
            return;
        
        context.Users.Add(new User { Role = 1, Active = true, Email = "fgandraf@gmail.com", PasswordHash = "12345senha" });
        context.Users.Add(new User { Role = 1, Active = true, Email = "fernanda@email.com", PasswordHash = "12345senha" });
        context.Users.Add(new User { Role = 1, Active = true, Email = "edson@seesp.com.br", PasswordHash = "12345senha" });
            
        context.SaveChanges();
    }

    private static void InsertProfessionals()
    {
        using var context = new PromoreDataContext(ConnectionString);
        
        if (context.Professionals.Any())
            return;
        
        context.Professionals.Add(new Professional { Name = "Felipe Ferreira Gandra", Cpf = "12345678900", Profession = "Arquiteto", UserId = 1 });
        context.Professionals.Add(new Professional { Name = "Fernanda Costa Garcia", Cpf = "98765432111", Profession = "Arquiteta", UserId = 2 });
        context.Professionals.Add(new Professional { Name = "Edson Gamba Ribeiro", Cpf = "13579024688", Profession = "Engenheiro Civil", UserId = 3 });
            
        context.SaveChanges();
    }
    
    private static void InsertRegion()
    {
        using var context = new PromoreDataContext(ConnectionString);
        
        if (context.Regions.Any())
            return;
        
        context.Regions.Add(new Region { Name = "Vila do Sucesso - Bauru/SP", EstablishedDate = new DateTime(2023, 12, 12), StartDate = new DateTime(2023, 02, 15), EndDate = new DateTime(2023, 12, 31)  });
        context.Regions.Add(new Region { Name = "Parque Jaraguá 2 - Bauru/SP", EstablishedDate = new DateTime(2023, 10, 26), StartDate = new DateTime(2023, 10, 26), EndDate = new DateTime(2024, 01, 12)  });
           
        context.SaveChanges();
    }
    
    private static void InsertLots()
    {
        using var context = new PromoreDataContext(ConnectionString);
        
        if (context.Lots.Any())
            return;
        
        context.Lots.Add(new Lot { Id = "A10", Block = "A", Number = 10, SurveyDate = new DateTime(2023, 10, 26), LastModifiedDate = DateTime.Now, Status  = 1, Comments = "Proprietário aguardando agendamento para atualizar o CADÚNICO", ProfessionalId = 1, RegionId = 1});
        context.Lots.Add(new Lot { Id = "A18",  Block = "A", Number = 18, SurveyDate = new DateTime(2023, 09, 12), LastModifiedDate = DateTime.Now, Status  = 1, Comments = "", ProfessionalId = 2, RegionId = 1});
        context.Lots.Add(new Lot { Id = "F25", Block = "F", Number = 25, SurveyDate = new DateTime(2023, 05, 4), LastModifiedDate = DateTime.Now, Status  = 1, Comments = "Imóvel alugado para Guilherme - (14) 99999-1500", ProfessionalId = 2, RegionId = 1});
        context.Lots.Add(new Lot { Id = "K5", Block = "K", Number = 5, SurveyDate = new DateTime(2023, 12, 22), LastModifiedDate = DateTime.Now, Status  = 1, Comments = "Proprietário não assina", ProfessionalId = 3, RegionId = 1});
        context.Lots.Add(new Lot { Id = "J32", Block = "J", Number = 32, SurveyDate = new DateTime(2023, 11, 17), LastModifiedDate = DateTime.Now, Status  = 1, Comments = "Casa de madeira", ProfessionalId = 1, RegionId = 1});
        
        context.SaveChanges();
    }
    
    
    private static void InsertClients()
    {
        using var context = new PromoreDataContext(ConnectionString);
        
        if (context.Clients.Any())
            return;
        
        context.Clients.Add(new Client { Name = "José Agripino da Silva", Cpf = "12345678900", Phone = "14999998888", MothersName = "Cleide Soares dos Santos", BirthdayDate = new DateTime(1980, 04, 2), LotId = "A10" });
        context.Clients.Add(new Client { Name = "Maria Tereza dos Santos", Cpf = "11122233344", Phone = "14988887777", MothersName = "Maria das Dores Carvalho", BirthdayDate = new DateTime(1978, 03, 9), LotId = "A18" });
        context.Clients.Add(new Client { Name = "Benedito Pereira Lima", Cpf = "10121231388", Phone = "14977776666", MothersName = "Silvana Rosa das Neves", BirthdayDate = new DateTime(1988, 07, 16), LotId = "F25" });
        context.Clients.Add(new Client { Name = "Mariano Palas Conceição", Cpf = "93382271166", Phone = "1498775555", MothersName = "Dolores Almeida da Silva", BirthdayDate = new DateTime(1990, 11, 19), LotId = "K5" });
        context.Clients.Add(new Client { Name = "Sonia Contijo Tavares", Cpf = "91929394955", Phone = "1498664444", MothersName = "Rita Amália de Jesus", BirthdayDate = new DateTime(1973, 09, 25), LotId = "J32" });
        
        context.SaveChanges();
    }
}