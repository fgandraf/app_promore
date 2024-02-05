using Promore.Core.Contexts.Client.Entity;
using Promore.Core.Contexts.Lot.Entity;
using Promore.Core.Contexts.Region.Entity;
using Promore.Core.Contexts.Role.Entity;
using Promore.Core.Contexts.User.Entity;
using Promore.Infra.Data;
using SecureIdentity.Password;

namespace Promore.Api.Extensions;

public static class WebApplicationExtensions
{
    public static void ConfigureFirstRun(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var dbInserts = scope.ServiceProvider.GetRequiredService<DbInserts>();
        dbInserts.InsertData();
    }
}

public class DbInserts
{
    private readonly PromoreDataContext _context;

        public DbInserts(PromoreDataContext dbContext)
            => _context = dbContext;
    
    
    public void InsertData()
    { 
        InsertRoles();
        InsertRegion();
        InsertUsers();
        InsertLots();
        InsertClients();
    }

    private void InsertRoles()
    {
        if (_context.Roles.Any())
            return;
        
        _context.Roles.Add(new Role { Name = "admin" });
        _context.Roles.Add(new Role { Name = "professional" });
        _context.Roles.Add(new Role { Name = "manager" });
            
        _context.SaveChanges();
    }
    
    private void InsertRegion()
    {
        if (_context.Regions.Any())
            return;
        
        _context.Regions.Add(new Region { Name = "Vila do Sucesso - Bauru/SP", EstablishedDate = new DateTime(2023, 12, 12), StartDate = new DateTime(2023, 02, 15), EndDate = new DateTime(2023, 12, 31)  });
        _context.Regions.Add(new Region { Name = "Parque Jaraguá 2 - Bauru/SP", EstablishedDate = new DateTime(2023, 10, 26), StartDate = new DateTime(2023, 10, 26), EndDate = new DateTime(2024, 01, 12)  });
        
        _context.SaveChanges();
    }
    
    private void InsertUsers()
    {
        if (_context.Users.Any())
            return;
        
        var user1 = new User
        {
            Active = true,
            Email = "admin@admin",
            Name = "Administrador",
            Cpf = "",
            PasswordHash =  PasswordHasher.Hash("admin"),
            Roles = 
            [
                _context.Roles.FirstOrDefault(x => x.Name == "Admin")
            ]
        };
        
        var user2 = new User
        {
            Active = true,
            Email = "fgandraf@gmail.com",
            PasswordHash =  PasswordHasher.Hash("12345senha"),
            Name = "Felipe Ferreira Gandra",
            Cpf = "12345678900",
            Profession = "Arquiteto",
            Roles = 
            [
                _context.Roles.FirstOrDefault(x => x.Name == "Professional")
            ],
            Regions =
            [
                _context.Regions.FirstOrDefault(x => x.Name == "Vila do Sucesso - Bauru/SP"),
                _context.Regions.FirstOrDefault(x => x.Name == "Parque Jaraguá 2 - Bauru/SP")
            ]
        };
        
        var user3 = new User 
        { 
            Active = true,  
            Email = "fernanda@email.com", 
            PasswordHash = PasswordHasher.Hash("12345senha"),
            Name = "Fernanda Costa Garcia", 
            Cpf = "98765432111", 
            Profession = "Arquiteta", 
            Roles = 
            [
                _context.Roles.FirstOrDefault(x => x.Name == "Professional")
            ],
            Regions =
            [
                _context.Regions.FirstOrDefault(x => x.Name == "Vila do Sucesso - Bauru/SP")
            ]
        };
        
        var user4 = new User
        {
            Active = true,
            Email = "edson@seesp.com.br",
            PasswordHash = PasswordHasher.Hash("12345senha"),
            Name = "Edson Gamba Ribeiro", 
            Cpf = "13579024688", 
            Profession = "Engenheiro Civil", 
            Roles = 
            [
                _context.Roles.FirstOrDefault(x => x.Name == "Professional"),
                _context.Roles.FirstOrDefault(x => x.Name == "Manager")
            ],
            Regions =
            [
                _context.Regions.FirstOrDefault(x => x.Name == "Parque Jaraguá 2 - Bauru/SP")
            ]
        };
        
        _context.Users.Add(user1);
        _context.Users.Add(user2);
        _context.Users.Add(user3);
        _context.Users.Add(user4);
        
        _context.SaveChanges();
    }
    
    private void InsertLots()
    {
         if (_context.Lots.Any())
             return;
         
         if (!_context.Users.Any() || !_context.Regions.Any())
             return;
         
         _context.Lots.Add(new Lot { Id = "A10", Block = "A", Number = 10, SurveyDate = new DateTime(2023, 10, 26), LastModifiedDate = DateTime.Now, Status  = 1, Comments = "Proprietário aguardando agendamento para atualizar o CADÚNICO", User = _context.Users.Skip(1).FirstOrDefault(), Region = _context.Regions.FirstOrDefault()});
         _context.Lots.Add(new Lot { Id = "A18",  Block = "A", Number = 18, SurveyDate = new DateTime(2023, 09, 12), LastModifiedDate = DateTime.Now, Status  = 2, Comments = "", User = _context.Users.Skip(2).FirstOrDefault(), Region = _context.Regions.FirstOrDefault()});
         _context.Lots.Add(new Lot { Id = "F25", Block = "F", Number = 25, SurveyDate = new DateTime(2023, 05, 4), LastModifiedDate = DateTime.Now, Status  = 1, Comments = "Imóvel alugado para Guilherme - (14) 99999-1500", User = _context.Users.Skip(3).FirstOrDefault(), Region = _context.Regions.FirstOrDefault()});
         _context.Lots.Add(new Lot { Id = "K5", Block = "K", Number = 5, SurveyDate = new DateTime(2023, 12, 22), LastModifiedDate = DateTime.Now, Status  = 3, Comments = "Proprietário não assina", User = _context.Users.Skip(2).FirstOrDefault(), Region = _context.Regions.FirstOrDefault()});
         _context.Lots.Add(new Lot { Id = "J32", Block = "J", Number = 32, SurveyDate = new DateTime(2023, 11, 17), LastModifiedDate = DateTime.Now, Status  = 1, Comments = "Casa de madeira", User = _context.Users.Skip(3).FirstOrDefault(), Region = _context.Regions.Skip(1).FirstOrDefault()});
         
         _context.SaveChanges();
    }
    
    private void InsertClients()
    {
         if (_context.Clients.Any())
             return;
         
         if (!_context.Lots.Any())
             return;
         
         _context.Clients.Add(new Client { Name = "José Agripino da Silva", Cpf = "12345678900", Phone = "14999998888", MothersName = "Cleide Soares dos Santos", BirthdayDate = new DateTime(1980, 04, 2), Lot = _context.Lots.FirstOrDefault(x => x.Id == "A10") });
         _context.Clients.Add(new Client { Name = "Maria Tereza dos Santos", Cpf = "11122233344", Phone = "14988887777", MothersName = "Maria das Dores Carvalho", BirthdayDate = new DateTime(1978, 03, 9), Lot = _context.Lots.FirstOrDefault(x => x.Id == "A18") });
         _context.Clients.Add(new Client { Name = "Benedito Pereira Lima", Cpf = "10121231388", Phone = "14977776666", MothersName = "Silvana Rosa das Neves", BirthdayDate = new DateTime(1988, 07, 16), Lot = _context.Lots.FirstOrDefault(x => x.Id == "F25") });
         _context.Clients.Add(new Client { Name = "Mariano Palas Conceição", Cpf = "93382271166", Phone = "1498775555", MothersName = "Dolores Almeida da Silva", BirthdayDate = new DateTime(1990, 11, 19), Lot = _context.Lots.FirstOrDefault(x => x.Id == "K5") });
         _context.Clients.Add(new Client { Name = "Sonia Contijo Tavares", Cpf = "91929394955", Phone = "1498664444", MothersName = "Rita Amália de Jesus", BirthdayDate = new DateTime(1973, 09, 25), Lot = _context.Lots.FirstOrDefault(x => x.Id == "A10") });
         
         _context.SaveChanges();
    }
}