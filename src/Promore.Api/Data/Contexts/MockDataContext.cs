using Moq;
using Moq.EntityFrameworkCore;
using Promore.Core.Models;
using SecureIdentity3.Password;

namespace Promore.Api.Data.Contexts;

public class MockDataContext
{
    public Mock<PromoreDataContext> Context { get; set; }
    
    public MockDataContext()
    {
        Initiate();

        var mockContext = new Mock<PromoreDataContext>();
        mockContext.Setup(m => m.Roles).ReturnsDbSet(_roles);
        mockContext.Setup(m => m.Regions).ReturnsDbSet(_regions);
        mockContext.Setup(m => m.Users).ReturnsDbSet(_users);
        mockContext.Setup(m => m.Clients).ReturnsDbSet(_clients);
        mockContext.Setup(m => m.Lots).ReturnsDbSet(_lots);
        
        Context = mockContext;
    }
    
    private List<Role> _roles = [];
    private List<User> _users = [];
    private List<Region> _regions = [];
    private List<Lot> _lots = [];
    private List<Client> _clients = [];
    
    private void Initiate()
    {
        #region Populate Roles

        _roles =
        [
            new Role { Id = 1, Name = "admin", Users = [] },
            new Role { Id = 2, Name = "professional", Users = [] },
            new Role { Id = 3, Name = "manager", Users = [] }
        ];

        #endregion

        #region Populate Regions

        _regions =
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

        #endregion

        #region Populate Users

        _users =
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
                    _roles.FirstOrDefault(x => x.Id == 1)!
                ],
                Regions = [],
                Lots = []
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
                    _roles.FirstOrDefault(x => x.Id == 2)!
                ],
                Regions =
                [
                    _regions.FirstOrDefault(x => x.Id == 1)!,
                    _regions.FirstOrDefault(x => x.Id == 2)!
                ],
                Lots = []
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
                    _roles.FirstOrDefault(x => x.Id == 2)!
                ],
                Regions =
                [
                    _regions.FirstOrDefault(x => x.Id == 1)!
                ],
                Lots = []
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
                    _roles.FirstOrDefault(x => x.Id == 2)!,
                    _roles.FirstOrDefault(x => x.Id == 3)!
                ],
                Regions =
                [
                    _regions.FirstOrDefault(x => x.Id == 1)!
                ],
                Lots = []
            }
        ];

        #endregion

        #region Populate Lots

        _lots =
        [
            new Lot
            {
                Id = 1, 
                Block = "A", 
                Number = 10,
                SurveyDate = new DateTime(2023, 10, 26),
                LastModifiedDate = DateTime.Now, 
                Status = 1,
                Comments = "Mock",
                UserId = 2,
                RegionId = 1,
                User = _users.FirstOrDefault(x => x.Id == 2)!,
                Region = _regions.FirstOrDefault(x => x.Id == 1)!
            },

            new Lot
            {
                Id = 2, Block = "A", Number = 18, SurveyDate = new DateTime(2023, 09, 12),
                LastModifiedDate = DateTime.Now, Status = 2, Comments = "Mock",
                User = _users.FirstOrDefault(x => x.Id == 3)!,
                Region = _regions.FirstOrDefault(x => x.Id == 1)!,
                RegionId = 1, UserId = 3
            },

            new Lot
            {
                Id = 3, Block = "F", Number = 25, SurveyDate = new DateTime(2023, 05, 4),
                LastModifiedDate = DateTime.Now, Status = 1,
                Comments = "Mock",
                User = _users.FirstOrDefault(x => x.Id == 4)!,
                Region = _regions.FirstOrDefault(x => x.Id == 1)!,
                RegionId = 1, UserId = 4
            },

            new Lot
            {
                Id = 4, Block = "K", Number = 5, SurveyDate = new DateTime(2023, 12, 22),
                LastModifiedDate = DateTime.Now, Status = 3, Comments = "Mock",
                User = _users.FirstOrDefault(x => x.Id == 3)!,
                Region = _regions.FirstOrDefault(x => x.Id == 1)!,
                RegionId = 1, UserId = 3
            },

            new Lot
            {
                Id = 5, Block = "J", Number = 32, SurveyDate = new DateTime(2023, 11, 17),
                LastModifiedDate = DateTime.Now, Status = 1, Comments = "Mock",
                User = _users.FirstOrDefault(x => x.Id == 4)!,
                Region = _regions.FirstOrDefault(x => x.Id == 2)!,
                RegionId = 2, UserId = 4
            }
        ];

        #endregion

        #region AddLotsToUsers

        var user2 = _users.FirstOrDefault(x => x.Id == 2);
        user2!.Lots =
        [
            _lots.FirstOrDefault(x => x.Id == 1)!
        ];

        var user3 = _users.FirstOrDefault(x => x.Id == 3);
        user3!.Lots =
        [
            _lots.FirstOrDefault(x => x.Id == 2)!,
            _lots.FirstOrDefault(x => x.Id == 4)!
        ];

        var user4 = _users.FirstOrDefault(x => x.Id == 4);
        user4!.Lots =
        [
            _lots.FirstOrDefault(x => x.Id == 3)!,
            _lots.FirstOrDefault(x => x.Id == 5)!
        ];

        #endregion

        #region AddLotsToRegions

        var regionVilaDoSucesso = _regions.FirstOrDefault(x => x.Id == 1);
        regionVilaDoSucesso!.Lots =
        [
            _lots.FirstOrDefault(x => x.Id == 1)!,
            _lots.FirstOrDefault(x => x.Id == 2)!,
            _lots.FirstOrDefault(x => x.Id == 3)!,
            _lots.FirstOrDefault(x => x.Id == 4)!
        ];

        var regionParqueJaragua = _regions.FirstOrDefault(x => x.Id == 2);
        regionParqueJaragua!.Lots =
        [
            _lots.FirstOrDefault(x => x.Id == 5)!
        ];

        #endregion

        #region Populate Clients

        _clients =
        [
            new Client
            {
                Id = 1,
                Name = "José Agripino da Silva Mock", Cpf = "12345678900", Phone = "14999998888",
                MothersName = "Cleide Soares dos Santos", BirthdayDate = new DateTime(1980, 04, 2),
                Lot = _lots.FirstOrDefault(x => x.Id == 1)!,
                LotId = 1
            },


            new Client
            {
                Id = 2,
                Name = "Maria Tereza dos Santos Mock", Cpf = "11122233344", Phone = "14988887777",
                MothersName = "Maria das Dores Carvalho", BirthdayDate = new DateTime(1978, 03, 9),
                Lot = _lots.FirstOrDefault(x => x.Id == 2)!,
                LotId = 2
            },


            new Client
            {
                Id = 3,
                Name = "Benedito Pereira Lima Mock", Cpf = "10121231388", Phone = "14977776666",
                MothersName = "Silvana Rosa das Neves", BirthdayDate = new DateTime(1988, 07, 16),
                Lot = _lots.FirstOrDefault(x => x.Id == 3)!,
                LotId = 3
            },


            new Client
            {
                Id = 4,
                Name = "Mariano Palas Conceição Mock", Cpf = "93382271166", Phone = "14987755555",
                MothersName = "Dolores Almeida da Silva", BirthdayDate = new DateTime(1990, 11, 19),
                Lot = _lots.FirstOrDefault(x => x.Id == 4)!,
                LotId = 4
            },


            new Client
            {
                Id = 5,
                Name = "Sonia Contijo Tavares Mock", Cpf = "91929394955", Phone = "14986644444",
                MothersName = "Rita Amália de Jesus", BirthdayDate = new DateTime(1973, 09, 25),
                Lot = _lots.FirstOrDefault(x => x.Id == 1)!,
                LotId = 1
            }
        ];

        #endregion

        #region AddClientToLots

        var lotA10 = _lots.FirstOrDefault(x => x.Id == 1);
        lotA10!.Clients =
        [
            _clients.FirstOrDefault(x => x.Id == 1)!,
            _clients.FirstOrDefault(x => x.Id == 5)!
        ];

        var lotA18 = _lots.FirstOrDefault(x => x.Id == 2);
        lotA18!.Clients =
        [
            _clients.FirstOrDefault(x => x.Id == 2)!
        ];

        var lotF25 = _lots.FirstOrDefault(x => x.Id == 3);
        lotF25!.Clients =
        [
            _clients.FirstOrDefault(x => x.Id == 3)!
        ];

        var lotK5 = _lots.FirstOrDefault(x => x.Id == 4);
        lotK5!.Clients =
        [
            _clients.FirstOrDefault(x => x.Id == 4)!
        ];

        #endregion
    }
}