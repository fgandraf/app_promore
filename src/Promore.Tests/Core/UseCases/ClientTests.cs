using Promore.Infra.Repositories.Mock;

namespace Promore.Tests.Core.UseCases;

[TestClass]
public class ClientTests
{
    private MockContext _context;
    private ClientRepositoryMock _clientRepository;
    private LotRepositoryMock _lotRepository;

    private const string NotExistentCpf = "98765432100";
    private const int ExistentClientId = 5;
    
    [TestInitialize]
    public void TestInitialize()
    {
        _context = new MockContext();
        _clientRepository = new ClientRepositoryMock(_context);
        _lotRepository = new LotRepositoryMock(_context);
    }
    
    [TestMethod]
    [TestCategory("Create")]
    public void Create_Client_ReturnsTrue()
    {
        var model = BuildCreateClientRequest(NotExistentCpf, existentLotId:"A10");
        
        var clientNotExists = !_context.Clients.Exists(x => x.Cpf == model.Cpf);
        var result = new Promore.Core.Contexts.ClientContext.UseCases.Create.Handler(_clientRepository,_lotRepository).Handle(model).Result;
        var created = _context.Clients.Exists(x => x.Id == result.Value);
        
        Assert.IsTrue(clientNotExists && result.Success && created);
    }
    
    [TestMethod]
    [TestCategory("Delete")]
    public void Delete_Client_ReturnsTrue()
    {
        var clientExists = _context.Clients.Exists(x => x.Id == ExistentClientId);
        var result = new Promore.Core.Contexts.ClientContext.UseCases.Delete.Handler(_clientRepository).Handle(ExistentClientId).Result;
        var deleted = !_context.Clients.Exists(x => x.Id == ExistentClientId);
        
        Assert.IsTrue(clientExists && result.Success && deleted);
    }
    
    [TestMethod]
    [TestCategory("Get")]
    public void GetAll_Clients_SameCountThanMock()
    {
        var existentClients = _context.Clients.Count;
        
        var result = new Promore.Core.Contexts.ClientContext.UseCases.GetAll.Handler(_clientRepository).Handle().Result.Value;
        
        Assert.AreEqual(existentClients, result.Count);
    }
    
    [TestMethod]
    [TestCategory("Get")]
    public void GetAll_ClientsByLotId_SameCountThanMock()
    {
        const string existentLotId = "A10";
        var existentClientsInLotA10 = _context.Lots.FirstOrDefault(x => x.Id == existentLotId)!.Clients.Count;
        
        var result = new Promore.Core.Contexts.ClientContext.UseCases.GetAllByLotId.Handler(_clientRepository).Handle(existentLotId).Result.Value;
        
        Assert.AreEqual(existentClientsInLotA10, result.Count);
    }
    
    [TestMethod]
    [TestCategory("Get")]
    public void GetClient_ById_ReturnsNotNull()
    {
        var result = new Promore.Core.Contexts.ClientContext.UseCases.GetById.Handler(_clientRepository).Handle(ExistentClientId).Result.Value;
        
        Assert.IsNotNull(result);
    }
    
    [TestMethod]
    [TestCategory("Update")]
    public void Update_Client_ReturnsTrue()
    {
        var existentClientCpf = _context.Clients.FirstOrDefault(x => x.Id == ExistentClientId)!.Cpf;
        var model = BuildUpdateClientRequest(ExistentClientId, NotExistentCpf);
        
        var result = new Promore.Core.Contexts.ClientContext.UseCases.Update.UpdateHandler(_clientRepository, _lotRepository).Handle(model).Result;
        var updatedClientCpf = _context.Clients.FirstOrDefault(x => x.Id == ExistentClientId)!.Cpf;
        
        Assert.IsTrue(result.Success && updatedClientCpf != existentClientCpf);
    }
    

    private static Promore.Core.Contexts.ClientContext.UseCases.Create.CreateClientRequest BuildCreateClientRequest(string notExistentCpf, string existentLotId)
    {
        return new Promore.Core.Contexts.ClientContext.UseCases.Create.CreateClientRequest
        {
            Name = "Felipe Gandra",
            Cpf = notExistentCpf,
            Phone = "14998290103",
            MothersName = "Maria Gandra",
            BirthdayDate = DateTime.Parse("1984-03-01"),
            LotId = existentLotId
        };
    }
    
    private static Promore.Core.Contexts.ClientContext.UseCases.Update.UpdateClientRequest BuildUpdateClientRequest(int id, string notExistentCpf)
    {
        return new Promore.Core.Contexts.ClientContext.UseCases.Update.UpdateClientRequest
        {
            Id = id,
            Name = "Sonia Contijo Tavares Mock",
            Cpf = notExistentCpf,
            Phone = "14986644444",
            MothersName = "Rita Amália de Jesus",
            BirthdayDate = new DateTime(1973, 09, 25),
            LotId = "A10"
        };
    }
}