using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using PromoreApi.Data;
using PromoreApi.Models;

namespace PromoreApi;

public static class DbInserts
{
    public static void InsertData()
    { 
        InsertRoles();
        InsertRegion();
        InsertUsers();
        InsertProfessionals();
        InsertLots();
        InsertClients();
    }

    private static void InsertRoles()
    {
        using var context = new PromoreDataContext();
        if (context.Roles.Any())
            return;

        context.Roles.Add(new Role { Name = "Admin" });
        context.Roles.Add(new Role { Name = "Professional" });
        context.Roles.Add(new Role { Name = "Manager" });
            
        context.SaveChanges();
    }
    
    private static void InsertRegion()
    {
        using var context = new PromoreDataContext();
        
        if (context.Regions.Any())
            return;
        
        context.Regions.Add(new Region { Name = "Vila do Sucesso - Bauru/SP", EstablishedDate = new DateTime(2023, 12, 12), StartDate = new DateTime(2023, 02, 15), EndDate = new DateTime(2023, 12, 31)  });
        context.Regions.Add(new Region { Name = "Parque Jaraguá 2 - Bauru/SP", EstablishedDate = new DateTime(2023, 10, 26), StartDate = new DateTime(2023, 10, 26), EndDate = new DateTime(2024, 01, 12)  });
        
        context.SaveChanges();
    }
    
    private static void InsertUsers()
    {
        using var context = new PromoreDataContext();
        
        if (context.Users.Any())
            return;


        var user1 = new User
        {
            Active = true,
            Email = "fgandraf@gmail.com",
            PasswordHash = "12345senha",
            Roles = 
            [
                context.Roles.FirstOrDefault(x => x.Name == "Admin"),
                context.Roles.FirstOrDefault(x => x.Name == "Professional")
            ],
            Regions =
            [
                context.Regions.FirstOrDefault(x => x.Name == "Vila do Sucesso - Bauru/SP"),
                context.Regions.FirstOrDefault(x => x.Name == "Parque Jaraguá 2 - Bauru/SP")
            ]
        };
        
        var user2 = new User 
        { 
            Active = true,  
            Email = "fernanda@email.com", 
            PasswordHash = "12345senha",
            Roles = 
            [
                context.Roles.FirstOrDefault(x => x.Name == "Professional")
            ],
            Regions =
            [
                context.Regions.FirstOrDefault(x => x.Name == "Vila do Sucesso - Bauru/SP")
            ]
        };

        var user3 = new User
        {
            Active = true,
            Email = "edson@seesp.com.br",
            PasswordHash = "12345senha",
            Roles = 
            [
                context.Roles.FirstOrDefault(x => x.Name == "Professional"),
                context.Roles.FirstOrDefault(x => x.Name == "Manager")
            ],
            Regions =
            [
                context.Regions.FirstOrDefault(x => x.Name == "Parque Jaraguá 2 - Bauru/SP")
            ]
        };
        
        context.Users.Add(user1);
        context.Users.Add(user2);
        context.Users.Add(user3);
        
        context.SaveChanges();
        
        // Console.WriteLine($"Email: {felipe.Entity.Email}  |  Id: {felipe.Entity.Id}");
        // Console.WriteLine($"Email: {fernanda.Entity.Email}  |  Id: {fernanda.Entity.Id}");
        // Console.WriteLine($"Email: {edson.Entity.Email}  |  Id: {edson.Entity.Id}");
        
    }

    private static void InsertProfessionals()
    {
        using var context = new PromoreDataContext();
        
        if (context.Professionals.Any())
            return;
        
        if (!context.Users.Any())
            return;
        
        context.Professionals.Add(new Professional { Name = "Felipe Ferreira Gandra", Cpf = "12345678900", Profession = "Arquiteto", User = context.Users.FirstOrDefault() });
        context.Professionals.Add(new Professional { Name = "Fernanda Costa Garcia", Cpf = "98765432111", Profession = "Arquiteta", User = context.Users.Skip(1).FirstOrDefault() });
        context.Professionals.Add(new Professional { Name = "Edson Gamba Ribeiro", Cpf = "13579024688", Profession = "Engenheiro Civil", User = context.Users.Skip(2).FirstOrDefault() });
            
        context.SaveChanges();
    }
    
    private static void InsertLots()
    {
         using var context = new PromoreDataContext();
        
         if (context.Lots.Any())
             return;
         
         if (!context.Professionals.Any() || !context.Regions.Any())
             return;
        
         context.Lots.Add(new Lot { Id = "A10", Block = "A", Number = 10, SurveyDate = new DateTime(2023, 10, 26), LastModifiedDate = DateTime.Now, Status  = 1, Comments = "Proprietário aguardando agendamento para atualizar o CADÚNICO", Professional = context.Professionals.FirstOrDefault(), Region = context.Regions.FirstOrDefault()});
         context.Lots.Add(new Lot { Id = "A18",  Block = "A", Number = 18, SurveyDate = new DateTime(2023, 09, 12), LastModifiedDate = DateTime.Now, Status  = 2, Comments = "", Professional = context.Professionals.Skip(1).FirstOrDefault(), Region = context.Regions.FirstOrDefault()});
         context.Lots.Add(new Lot { Id = "F25", Block = "F", Number = 25, SurveyDate = new DateTime(2023, 05, 4), LastModifiedDate = DateTime.Now, Status  = 1, Comments = "Imóvel alugado para Guilherme - (14) 99999-1500", Professional = context.Professionals.FirstOrDefault(), Region = context.Regions.FirstOrDefault()});
         context.Lots.Add(new Lot { Id = "K5", Block = "K", Number = 5, SurveyDate = new DateTime(2023, 12, 22), LastModifiedDate = DateTime.Now, Status  = 3, Comments = "Proprietário não assina", Professional = context.Professionals.Skip(2).FirstOrDefault(), Region = context.Regions.FirstOrDefault()});
         context.Lots.Add(new Lot { Id = "J32", Block = "J", Number = 32, SurveyDate = new DateTime(2023, 11, 17), LastModifiedDate = DateTime.Now, Status  = 1, Comments = "Casa de madeira", Professional = context.Professionals.Skip(1).FirstOrDefault(), Region = context.Regions.Skip(1).FirstOrDefault()});
        
         context.SaveChanges();
    }
    
    private static void InsertClients()
    {
         using var context = new PromoreDataContext();
        
         if (context.Clients.Any())
             return;
        
         if (!context.Lots.Any())
             return;
        
         context.Clients.Add(new Client { Name = "José Agripino da Silva", Cpf = "12345678900", Phone = "14999998888", MothersName = "Cleide Soares dos Santos", BirthdayDate = new DateTime(1980, 04, 2), Lot = context.Lots.FirstOrDefault(x => x.Id == "A10") });
         context.Clients.Add(new Client { Name = "Maria Tereza dos Santos", Cpf = "11122233344", Phone = "14988887777", MothersName = "Maria das Dores Carvalho", BirthdayDate = new DateTime(1978, 03, 9), Lot = context.Lots.FirstOrDefault(x => x.Id == "A18") });
         context.Clients.Add(new Client { Name = "Benedito Pereira Lima", Cpf = "10121231388", Phone = "14977776666", MothersName = "Silvana Rosa das Neves", BirthdayDate = new DateTime(1988, 07, 16), Lot = context.Lots.FirstOrDefault(x => x.Id == "F25") });
         context.Clients.Add(new Client { Name = "Mariano Palas Conceição", Cpf = "93382271166", Phone = "1498775555", MothersName = "Dolores Almeida da Silva", BirthdayDate = new DateTime(1990, 11, 19), Lot = context.Lots.FirstOrDefault(x => x.Id == "K5") });
         context.Clients.Add(new Client { Name = "Sonia Contijo Tavares", Cpf = "91929394955", Phone = "1498664444", MothersName = "Rita Amália de Jesus", BirthdayDate = new DateTime(1973, 09, 25), Lot = context.Lots.FirstOrDefault(x => x.Id == "J32") });
        
         context.SaveChanges();
    }
}