// using Promore.Api.Data.Repositories.Mock;
// using Promore.Api.Handlers;
// using Promore.Core.Requests.Clients;
//
// namespace Promore.Tests.Api.Services;
//
// [TestClass]
// public class ClientServiceTests
// {
//     private readonly MockContext _context;
//     private readonly ClientHandlerMock _clientRepository;
//     private readonly LotHandlerMock _lotRepository;
//
//     public ClientServiceTests(MockContext context)
//     {
//         _context = context;
//         _clientRepository = new ClientHandlerMock(_context);
//         _lotRepository = new LotHandlerMock(_context);
//     }
//
//     private const string NotExistentCpf = "98765432100";
//     private const int ExistentClientId = 5;
//     
//     
//     [TestMethod]
//     [TestCategory("Create")]
//     public void Create_Client_ReturnsTrue()
//     {
//         var model = BuildCreateClientRequest(NotExistentCpf, existentLotId:1);
//         
//         var clientNotExists = !_context.Clients.Exists(x => x.Cpf == model.Cpf);
//         var result = new ClientService(_clientRepository, _lotRepository).CreateAsync(model).Result;
//         var created = _context.Clients.Exists(x => x.Id == result.Value);
//         
//         Assert.IsTrue(clientNotExists && result.Success && created);
//     }
//     
//     [TestMethod]
//     [TestCategory("Delete")]
//     public void Delete_Client_ReturnsTrue()
//     {
//         var clientExists = _context.Clients.Exists(x => x.Id == ExistentClientId);
//         var result = new ClientService(_clientRepository, _lotRepository).DeleteAsync(new DeleteClientRequest{Id = ExistentClientId}).Result;
//         var deleted = !_context.Clients.Exists(x => x.Id == ExistentClientId);
//         
//         Assert.IsTrue(clientExists && result.Success && deleted);
//     }
//     
//     [TestMethod]
//     [TestCategory("Get")]
//     public void GetAll_Clients_SameCountThanMock()
//     {
//         var existentClients = _context.Clients.Count;
//         
//         var result = new ClientService(_clientRepository, _lotRepository).GetAllAsync(new GetAllClientsRequest()).Result.Value;
//         
//         Assert.AreEqual(existentClients, result.Count);
//     }
//     
//     [TestMethod]
//     [TestCategory("Get")]
//     public void GetAll_ClientsByLotId_SameCountThanMock()
//     {
//         const int existentLotId = 1;
//         var existentClientsInLotA10 = _context.Lots.FirstOrDefault(x => x.Id == existentLotId)!.Clients!.Count;
//         
//         var result = new ClientService(_clientRepository, _lotRepository).GetAllByLotIdAsync(new GetAllClientsByLotIdRequest{LotId = existentLotId}).Result.Value;
//         
//         Assert.AreEqual(existentClientsInLotA10, result.Count);
//     }
//     
//     [TestMethod]
//     [TestCategory("Get")]
//     public void GetClient_ById_ReturnsNotNull()
//     {
//         var result = new ClientService(_clientRepository, _lotRepository).GetByIdAsync(new GetClientByIdRequest{Id = ExistentClientId}).Result.Value;
//         
//         Assert.IsNotNull(result);
//     }
//     
//     [TestMethod]
//     [TestCategory("Update")]
//     public void Update_Client_ReturnsTrue()
//     {
//         var existentClientCpf = _context.Clients.FirstOrDefault(x => x.Id == ExistentClientId)!.Cpf;
//         var model = BuildUpdateClientRequest(ExistentClientId, NotExistentCpf);
//         
//         var result = new ClientService(_clientRepository, _lotRepository).UpdateAsync(model).Result;
//         var updatedClientCpf = _context.Clients.FirstOrDefault(x => x.Id == ExistentClientId)!.Cpf;
//         
//         Assert.IsTrue(result.Success && updatedClientCpf != existentClientCpf);
//     }
//     
//
//     private static CreateClientRequest BuildCreateClientRequest(string notExistentCpf, int existentLotId)
//     {
//         return new CreateClientRequest
//         {
//             Name = "Felipe Gandra",
//             Cpf = notExistentCpf,
//             Phone = "14998290103",
//             MothersName = "Maria Gandra",
//             BirthdayDate = DateTime.Parse("1984-03-01"),
//             LotId = existentLotId
//         };
//     }
//     
//     private static UpdateClientRequest BuildUpdateClientRequest(int id, string notExistentCpf)
//     {
//         return new UpdateClientRequest
//         {
//             Id = id,
//             Name = "Sonia Contijo Tavares Mock",
//             Cpf = notExistentCpf,
//             Phone = "14986644444",
//             MothersName = "Rita Amália de Jesus",
//             BirthdayDate = new DateTime(1973, 09, 25),
//             LotId = 1
//         };
//     }
// }