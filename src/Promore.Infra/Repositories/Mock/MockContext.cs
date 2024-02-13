using Promore.Core.Contexts.ClientContext.Entities;
using Promore.Core.Contexts.LotContext.Entities;
using Promore.Core.Contexts.RegionContext.Entities;
using Promore.Core.Contexts.RoleContext.Entities;
using Promore.Core.Contexts.UserContext.Entities;
using SecureIdentity.Password;

namespace Promore.Infra.Repositories.Mock;

public class MockContext
{
    public List<Role> Roles { get; set; }
    public List<User> Users { get; set; }
    public List<Region> Regions { get; set; }
    public List<Lot> Lots { get; set; }
    public List<Client> Clients { get; set; }
    
    
    public MockContext()
    {
        InitializeRoles();
        InitializeRegions();
        InitializeUsers();
        InitializeLots();
        AddLotsToUsers();
        AddLotsToRegions();
        InitializeClients();
        AddClientsToLots();
    }


    private void InitializeRoles()
    {
        Roles = new List<Role>
        {
            new Role { Id = 1, Name = "admin" },
            new Role { Id = 2, Name = "professional" },
            new Role { Id = 3, Name = "manager" }
        };
    }
    
    private void InitializeRegions()
    {
        Regions =
        [
            new Region
            {
                Id = 1,
                Name = "Vila do Sucesso - Bauru/SP Mock",
                EstablishedDate = new DateTime(2023, 12, 12),
                StartDate = new DateTime(2023, 02, 15),
                EndDate = new DateTime(2023, 12, 31)
            },

            new Region
            {
                Id = 2,
                Name = "Parque Jaraguá 2 - Bauru/SP Mock",
                EstablishedDate = new DateTime(2023, 10, 26),
                StartDate = new DateTime(2023, 10, 26),
                EndDate = new DateTime(2024, 01, 12)
            }
        ];
    }
    
    private void InitializeUsers()
    {
            Users = new List<User>
            {
                new User
                {
                    Id = 1,
                    Active = true,
                    Email = "admin@admin",
                    Name = "Administrador Mock",
                    Cpf = "12345678900",
                    PasswordHash = PasswordHasher.Hash("admin"),
                    Roles =
                    [
                        Roles.FirstOrDefault(x => x.Name == "admin")
                    ],
                    Regions = []
                },
        
                new User
                {
                    Id = 2,
                    Active = true,
                    Email = "fgandraf@gmail.com",
                    PasswordHash = PasswordHasher.Hash("12345senha"),
                    Name = "Felipe Ferreira Gandra Mock",
                    Cpf = "12345678900",
                    Profession = "Arquiteto",
                    Roles =
                    [
                        Roles.FirstOrDefault(x => x.Name == "professional")
                    ],
                    Regions = new List<Region>
                    {
                        Regions.FirstOrDefault(x => x.Name == "Vila do Sucesso - Bauru/SP Mock"),
                        Regions.FirstOrDefault(x => x.Name == "Parque Jaraguá 2 - Bauru/SP Mock")
                    }
                },
            
                new User
                {
                    Id = 3,
                    Active = true,
                    Email = "fernanda@email.com",
                    PasswordHash = PasswordHasher.Hash("12345senha"),
                    Name = "Fernanda Costa Garcia Mock",
                    Cpf = "98765432111",
                    Profession = "Arquiteta",
                    Roles =
                    [
                        Roles.FirstOrDefault(x => x.Name == "professional")
                    ],
                    Regions = 
                    [
                        Regions.FirstOrDefault(x => x.Name == "Vila do Sucesso - Bauru/SP Mock")
                    ]
                },
            
                new User
                {
                    Id = 4,
                    Active = true,
                    Email = "edson@seesp.com.br",
                    PasswordHash = PasswordHasher.Hash("12345senha"),
                    Name = "Edson Gamba Ribeiro Mock",
                    Cpf = "13579024688",
                    Profession = "Engenheiro Civil",
                    Roles =
                    [
                        Roles.FirstOrDefault(x => x.Name == "professional"),
                        Roles.FirstOrDefault(x => x.Name == "manager")
                    ],
                    Regions = 
                    [
                        Regions.FirstOrDefault(x => x.Name == "Parque Jaraguá 2 - Bauru/SP Mock")
                    ]
                }
            };
    }
    
    private void InitializeLots()
    {
        Lots = new List<Lot>
        {
            new Lot
            {
                Id = "A10", Block = "A", Number = 10, SurveyDate = new DateTime(2023, 10, 26),
                LastModifiedDate = DateTime.Now, Status = 1,
                Comments = "Mock",
                User = Users.FirstOrDefault(x => x.Id == 2), 
                Region = Regions.FirstOrDefault(x => x.Id == 1),
                RegionId = 1, UserId = 2
            },
                
            new Lot
            {
                Id = "A18", Block = "A", Number = 18, SurveyDate = new DateTime(2023, 09, 12),
                LastModifiedDate = DateTime.Now, Status = 2, Comments = "Mock", 
                User = Users.FirstOrDefault(x => x.Id == 3),
                Region = Regions.FirstOrDefault(x => x.Id == 1),
                RegionId = 1, UserId = 3
            },
                
            new Lot
            {
                Id = "F25", Block = "F", Number = 25, SurveyDate = new DateTime(2023, 05, 4),
                LastModifiedDate = DateTime.Now, Status = 1,
                Comments = "Mock",
                User = Users.FirstOrDefault(x => x.Id == 4), 
                Region = Regions.FirstOrDefault(x => x.Id == 1),
                RegionId = 1, UserId = 4
            },
                    
            new Lot
            {
                Id = "K5", Block = "K", Number = 5, SurveyDate = new DateTime(2023, 12, 22),
                LastModifiedDate = DateTime.Now, Status = 3, Comments = "Mock",
                User = Users.FirstOrDefault(x => x.Id == 3),
                Region = Regions.FirstOrDefault(x => x.Id == 1),
                RegionId = 1, UserId = 3
            },
                
            new Lot
            {
                Id = "J32", Block = "J", Number = 32, SurveyDate = new DateTime(2023, 11, 17),
                LastModifiedDate = DateTime.Now, Status = 1, Comments = "Mock",
                User = Users.FirstOrDefault(x => x.Id == 4), 
                Region = Regions.FirstOrDefault(x => x.Id == 2),
                RegionId = 2, UserId = 4
            }
        };
    }

    private void AddLotsToUsers()
    {
        var user2 = Users.FirstOrDefault(x => x.Id == 2);
        user2.Lots =
        [
            Lots.FirstOrDefault(x => x.Id == "A10")
        ];
        
        var user3 = Users.FirstOrDefault(x => x.Id == 3);
        user3.Lots =
        [
            Lots.FirstOrDefault(x => x.Id == "A18"),
            Lots.FirstOrDefault(x => x.Id == "K5")
        ];
        
        var user4 = Users.FirstOrDefault(x => x.Id == 4);
        user4.Lots =
        [
            Lots.FirstOrDefault(x => x.Id == "F25"),
            Lots.FirstOrDefault(x => x.Id == "J32")
        ];
    }
    
    private void AddLotsToRegions()
    {
        var regionVilaDoSucesso = Regions.FirstOrDefault(x => x.Id == 1);
        regionVilaDoSucesso!.Lots =
        [
            Lots.FirstOrDefault(x => x.Id == "A10"),
            Lots.FirstOrDefault(x => x.Id == "A18"),
            Lots.FirstOrDefault(x => x.Id == "F25"),
            Lots.FirstOrDefault(x => x.Id == "K5")
        ];
        
        var regionParqueJaragua = Regions.FirstOrDefault(x => x.Id == 2);
        regionParqueJaragua!.Lots = [
            Lots.FirstOrDefault(x => x.Id == "J32")
        ];
    }
    
    private void InitializeClients()
    {
        Clients = new List<Client>
        {
            new Client
            {
                Id = 1,
                Name = "José Agripino da Silva Mock", Cpf = "12345678900", Phone = "14999998888",
                MothersName = "Cleide Soares dos Santos", BirthdayDate = new DateTime(1980, 04, 2),
                Lot = Lots.FirstOrDefault(x => x.Id == "A10"),
                LotId = "A10"
            },
                
            new Client
            {
                Id = 2,
                Name = "Maria Tereza dos Santos Mock", Cpf = "11122233344", Phone = "14988887777",
                MothersName = "Maria das Dores Carvalho", BirthdayDate = new DateTime(1978, 03, 9),
                Lot = Lots.FirstOrDefault(x => x.Id == "A18"),
                LotId = "A18"
            },
                
            new Client
            {
                Id = 3,
                Name = "Benedito Pereira Lima Mock", Cpf = "10121231388", Phone = "14977776666",
                MothersName = "Silvana Rosa das Neves", BirthdayDate = new DateTime(1988, 07, 16),
                Lot = Lots.FirstOrDefault(x => x.Id == "F25"),
                LotId = "F25"
            },
                
            new Client
            {
                Id = 4,
                Name = "Mariano Palas Conceição Mock", Cpf = "93382271166", Phone = "1498775555",
                MothersName = "Dolores Almeida da Silva", BirthdayDate = new DateTime(1990, 11, 19),
                Lot = Lots.FirstOrDefault(x => x.Id == "K5"),
                LotId = "K5"
            },
                
            new Client
            {
                Id = 5,
                Name = "Sonia Contijo Tavares Mock", Cpf = "91929394955", Phone = "1498664444",
                MothersName = "Rita Amália de Jesus", BirthdayDate = new DateTime(1973, 09, 25),
                Lot = Lots.FirstOrDefault(x => x.Id == "A10"),
                LotId = "A10"
            }
        };
    }

    private void AddClientsToLots()
    {
        var lotA10 = Lots.FirstOrDefault(x => x.Id == "A10");
        lotA10!.Clients =
        [
            Clients.FirstOrDefault(x => x.Id == 1),
            Clients.FirstOrDefault(x => x.Id == 5)
        ];
        
        var lotA18 = Lots.FirstOrDefault(x => x.Id == "A18");
        lotA18!.Clients =
        [
            Clients.FirstOrDefault(x => x.Id == 2)
        ];
        
        var lotF25 = Lots.FirstOrDefault(x => x.Id == "F25");
        lotF25!.Clients =
        [
            Clients.FirstOrDefault(x => x.Id == 3)
        ];
        
        var lotK5 = Lots.FirstOrDefault(x => x.Id == "K5");
        lotK5!.Clients =
        [
            Clients.FirstOrDefault(x => x.Id == 4)
        ];
    }
}