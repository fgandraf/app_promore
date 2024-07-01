using Promore.Core.Models;
using SecureIdentity3.Password;

namespace Promore.Tests.Mock;

public class MockContext
{
    public List<Role> Roles { get; set; } = [];
    public List<User> Users { get; set; } = [];
    public List<Region> Regions { get; set; } = [];
    public List<Lot> Lots { get; set; } = [];
    public List<Client> Clients { get; set; } = [];
    
    
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
        Roles =
        [
            new Role { Id = 1, Name = "admin" },
            new Role { Id = 2, Name = "professional" },
            new Role { Id = 3, Name = "manager" }
        ];
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
            Users =
            [
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
                        Roles.FirstOrDefault(x => x.Name == "admin")!
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
                        Roles.FirstOrDefault(x => x.Name == "professional")!
                    ],
                    Regions =
                    [
                        Regions.FirstOrDefault(x => x.Name == "Vila do Sucesso - Bauru/SP Mock")!,
                        Regions.FirstOrDefault(x => x.Name == "Parque Jaraguá 2 - Bauru/SP Mock")!
                    ]
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
                        Roles.FirstOrDefault(x => x.Name == "professional")!
                    ],
                    Regions =
                    [
                        Regions.FirstOrDefault(x => x.Name == "Vila do Sucesso - Bauru/SP Mock")!
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
                        Roles.FirstOrDefault(x => x.Name == "professional")!,
                        Roles.FirstOrDefault(x => x.Name == "manager")!
                    ],
                    Regions =
                    [
                        Regions.FirstOrDefault(x => x.Name == "Parque Jaraguá 2 - Bauru/SP Mock")!
                    ]
                }
            ];
    }
    
    private void InitializeLots()
    {
        Lots =
        [
            new Lot
            {
                Id = 1, Block = "A", Number = 10, SurveyDate = new DateTime(2023, 10, 26),
                LastModifiedDate = DateTime.Now, Status = 1,
                Comments = "Mock",
                User = Users.FirstOrDefault(x => x.Id == 2)!,
                Region = Regions.FirstOrDefault(x => x.Id == 1)!,
                RegionId = 1, UserId = 2
            },


            new Lot
            {
                Id = 2, Block = "A", Number = 18, SurveyDate = new DateTime(2023, 09, 12),
                LastModifiedDate = DateTime.Now, Status = 2, Comments = "Mock",
                User = Users.FirstOrDefault(x => x.Id == 3)!,
                Region = Regions.FirstOrDefault(x => x.Id == 1)!,
                RegionId = 1, UserId = 3
            },


            new Lot
            {
                Id = 3, Block = "F", Number = 25, SurveyDate = new DateTime(2023, 05, 4),
                LastModifiedDate = DateTime.Now, Status = 1,
                Comments = "Mock",
                User = Users.FirstOrDefault(x => x.Id == 4)!,
                Region = Regions.FirstOrDefault(x => x.Id == 1)!,
                RegionId = 1, UserId = 4
            },


            new Lot
            {
                Id = 4, Block = "K", Number = 5, SurveyDate = new DateTime(2023, 12, 22),
                LastModifiedDate = DateTime.Now, Status = 3, Comments = "Mock",
                User = Users.FirstOrDefault(x => x.Id == 3)!,
                Region = Regions.FirstOrDefault(x => x.Id == 1)!,
                RegionId = 1, UserId = 3
            },


            new Lot
            {
                Id = 5, Block = "J", Number = 32, SurveyDate = new DateTime(2023, 11, 17),
                LastModifiedDate = DateTime.Now, Status = 1, Comments = "Mock",
                User = Users.FirstOrDefault(x => x.Id == 4)!,
                Region = Regions.FirstOrDefault(x => x.Id == 2)!,
                RegionId = 2, UserId = 4
            }
        ];
    }

    private void AddLotsToUsers()
    {
        var user2 = Users.FirstOrDefault(x => x.Id == 2);
        user2!.Lots =
        [
            Lots.FirstOrDefault(x => x.Id == 1)!
        ];
        
        var user3 = Users.FirstOrDefault(x => x.Id == 3);
        user3!.Lots =
        [
            Lots.FirstOrDefault(x => x.Id == 2)!,
            Lots.FirstOrDefault(x => x.Id == 4)!
        ];
        
        var user4 = Users.FirstOrDefault(x => x.Id == 4);
        user4!.Lots =
        [
            Lots.FirstOrDefault(x => x.Id == 3)!,
            Lots.FirstOrDefault(x => x.Id == 5)!
        ];
    }
    
    private void AddLotsToRegions()
    {
        var regionVilaDoSucesso = Regions.FirstOrDefault(x => x.Id == 1);
        regionVilaDoSucesso!.Lots =
        [
            Lots.FirstOrDefault(x => x.Id == 1)!,
            Lots.FirstOrDefault(x => x.Id == 2)!,
            Lots.FirstOrDefault(x => x.Id == 3)!,
            Lots.FirstOrDefault(x => x.Id == 4)!
        ];
        
        var regionParqueJaragua = Regions.FirstOrDefault(x => x.Id == 2);
        regionParqueJaragua!.Lots = [
            Lots.FirstOrDefault(x => x.Id == 5)!
        ];
    }
    
    private void InitializeClients()
    {
        Clients =
        [
            new Client
            {
                Id = 1,
                Name = "José Agripino da Silva Mock", Cpf = "12345678900", Phone = "14999998888",
                MothersName = "Cleide Soares dos Santos", BirthdayDate = new DateTime(1980, 04, 2),
                Lot = Lots.FirstOrDefault(x => x.Id == 1)!,
                LotId = 1
            },


            new Client
            {
                Id = 2,
                Name = "Maria Tereza dos Santos Mock", Cpf = "11122233344", Phone = "14988887777",
                MothersName = "Maria das Dores Carvalho", BirthdayDate = new DateTime(1978, 03, 9),
                Lot = Lots.FirstOrDefault(x => x.Id == 2)!,
                LotId = 2
            },


            new Client
            {
                Id = 3,
                Name = "Benedito Pereira Lima Mock", Cpf = "10121231388", Phone = "14977776666",
                MothersName = "Silvana Rosa das Neves", BirthdayDate = new DateTime(1988, 07, 16),
                Lot = Lots.FirstOrDefault(x => x.Id == 3)!,
                LotId = 3
            },


            new Client
            {
                Id = 4,
                Name = "Mariano Palas Conceição Mock", Cpf = "93382271166", Phone = "14987755555",
                MothersName = "Dolores Almeida da Silva", BirthdayDate = new DateTime(1990, 11, 19),
                Lot = Lots.FirstOrDefault(x => x.Id == 4)!,
                LotId = 4
            },


            new Client
            {
                Id = 5,
                Name = "Sonia Contijo Tavares Mock", Cpf = "91929394955", Phone = "14986644444",
                MothersName = "Rita Amália de Jesus", BirthdayDate = new DateTime(1973, 09, 25),
                Lot = Lots.FirstOrDefault(x => x.Id == 1)!,
                LotId = 1
            }
        ];
    }

    private void AddClientsToLots()
    {
        var lotA10 = Lots.FirstOrDefault(x => x.Id == 1);
        lotA10!.Clients =
        [
            Clients.FirstOrDefault(x => x.Id == 1)!,
            Clients.FirstOrDefault(x => x.Id == 5)!
        ];
        
        var lotA18 = Lots.FirstOrDefault(x => x.Id == 2);
        lotA18!.Clients =
        [
            Clients.FirstOrDefault(x => x.Id == 2)!
        ];
        
        var lotF25 = Lots.FirstOrDefault(x => x.Id == 3);
        lotF25!.Clients =
        [
            Clients.FirstOrDefault(x => x.Id == 3)!
        ];
        
        var lotK5 = Lots.FirstOrDefault(x => x.Id == 4);
        lotK5!.Clients =
        [
            Clients.FirstOrDefault(x => x.Id == 4)!
        ];
    }
}