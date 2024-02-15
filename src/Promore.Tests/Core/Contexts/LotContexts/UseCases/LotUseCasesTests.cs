using Promore.Infra.Repositories.Mock;

namespace Promore.Tests.Core.Contexts.LotContexts.UseCases;

[TestClass]
public class LotUseCasesTests
{
    private MockContext? _context;
    private LotRepositoryMock? _lotRepository;
    private UserRepositoryMock? _userRepository;
    private RegionRepositoryMock? _regionRepository;
    private ClientRepositoryMock? _clientRepository;
    
    private const string ExistentLotId = "A10";
    private const int ExistentUserId = 2;
    private const int ExistentRegionId = 1;
    private const string NotExistentLotId = "Z60";
    
    [TestInitialize]
    public void TestInitialize()
    {
        _context = new MockContext();
        _lotRepository = new LotRepositoryMock(_context);
        _userRepository = new UserRepositoryMock(_context);
        _regionRepository = new RegionRepositoryMock(_context);
        _clientRepository = new ClientRepositoryMock(_context);
    }

    [TestMethod]
    [TestCategory("Create")]
    public void Create_Lot_ReturnsTrue()
    {
        var model = BuildCreateLotRequest(NotExistentLotId, ExistentUserId, ExistentRegionId);
        
        var lotNotExists = !_context!.Lots.Exists(x => x.Id == model.Id);
        var result = new Promore.Core.Contexts.LotContext.UseCases.Create.Hander(_lotRepository, _userRepository, _regionRepository).Handle(model).Result;
        var created = _context.Lots.Exists(x => x.Id == result.Value);
        
        Assert.IsTrue(lotNotExists && result.Success && created);
    }

    [TestMethod]
    public void Delete_Lot_ReturnsTrue()
    {
        var lotExists = _context!.Lots.Exists(x => x.Id == ExistentLotId);
        var result = new Promore.Core.Contexts.LotContext.UseCases.Delete.Handler(_lotRepository).Handle(ExistentLotId).Result;
        var deleted = !_context.Lots.Exists(x => x.Id == ExistentLotId);
        
        Assert.IsTrue(lotExists && result.Success && deleted);
    }

    [TestMethod]
    [TestCategory("Get")]
    public void GetLot_ById_ReturnsNotNull()
    {
        var result = new Promore.Core.Contexts.LotContext.UseCases.GetById.Handler(_lotRepository).Handle(ExistentLotId).Result.Value;
        
        Assert.IsNotNull(result);
    }

    [TestMethod]
    [TestCategory("Get")]
    public void GetLotStatus_ByRegion_ReturnsNotNull()
    {
        var existentLotsInRegion = _context!.Lots.Where(x => x.RegionId == ExistentRegionId).ToList().Count;
        
        var result = new Promore.Core.Contexts.LotContext.UseCases.GetStatusByRegion.Handler(_lotRepository).Handle(ExistentRegionId).Result;

        Assert.AreEqual(existentLotsInRegion, result.Value.Count);
    }

    [TestMethod]
    [TestCategory("Update")]
    public void Update_Lot_ReturnsTrue()
    {
        var existentLotStatus = _context!.Lots.FirstOrDefault(x => x.Id == ExistentLotId)!.Status;
        var model = BuildUpdateLotRequest(ExistentLotId, newStatus: existentLotStatus + 1);
        
        var result = new Promore.Core.Contexts.LotContext.UseCases.Update.Handler(_lotRepository, _userRepository, _regionRepository, _clientRepository).Handle(model).Result;
        var updatedLotStatus = _context.Lots.FirstOrDefault(x => x.Id == ExistentLotId)!.Status;
        
        Assert.IsTrue(result.Success && updatedLotStatus != existentLotStatus);
    }

    private static Promore.Core.Contexts.LotContext.UseCases.Create.CreateLotRequest BuildCreateLotRequest(string notExistentLotId, int existentUserId, int existentRegionId)
    {
        return new Promore.Core.Contexts.LotContext.UseCases.Create.CreateLotRequest
        {
            Id = notExistentLotId,
            SurveyDate = new DateTime(2023, 12, 22),
            LastModifiedDate = new DateTime(2024, 01, 16),
            Status = 1,
            Comments = "",
            UserId = existentUserId,
            RegionId = existentRegionId
        };
    }
    
    private static Promore.Core.Contexts.LotContext.UseCases.Update.UpdateLotRequest BuildUpdateLotRequest(string existentLotId, int newStatus)
    {
        return new Promore.Core.Contexts.LotContext.UseCases.Update.UpdateLotRequest
        {
            Id = existentLotId,
            SurveyDate = new DateTime(2023, 12, 22),
            LastModifiedDate = new DateTime(2024, 01, 16),
            Status = newStatus,
            Comments = "",
            UserId = ExistentUserId,
            RegionId = ExistentRegionId,
            Clients = [1,5]
        };
    }
}