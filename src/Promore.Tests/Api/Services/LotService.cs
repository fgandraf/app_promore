using Promore.Api.Services;
using Promore.Core.Requests.Lots;
using Promore.Infra.Repositories.Mock;

namespace Promore.Tests.Api.Services;

[TestClass]
public class LotService
{
    private MockContext? _context;
    private LotHandlerMock? _lotRepository;
    private UserHandlerMock? _userRepository;
    private RegionHandlerMock? _regionRepository;
    private ClientHandlerMock? _clientRepository;
    
    private const string ExistentLotId = "A10";
    private const int ExistentUserId = 2;
    private const int ExistentRegionId = 1;
    private const string NotExistentLotId = "Z60";
    
    [TestInitialize]
    public void TestInitialize()
    {
        _context = new MockContext();
        _lotRepository = new LotHandlerMock(_context);
        _userRepository = new UserHandlerMock(_context);
        _regionRepository = new RegionHandlerMock(_context);
        _clientRepository = new ClientHandlerMock(_context);
    }

    [TestMethod]
    [TestCategory("Create")]
    public void Create_Lot_ReturnsTrue()
    {
        var model = BuildCreateLotRequest(NotExistentLotId, ExistentUserId, ExistentRegionId);
        
        var lotNotExists = !_context!.Lots.Exists(x => x.Id == model.Id);
        var result = new Promore.Api.Services.LotService(_lotRepository, _regionRepository, _clientRepository, _userRepository).CreateAsync(model).Result;
        var created = _context.Lots.Exists(x => x.Id == result.Value);
        
        Assert.IsTrue(lotNotExists && result.Success && created);
    }

    [TestMethod]
    public void Delete_Lot_ReturnsTrue()
    {
        var lotExists = _context!.Lots.Exists(x => x.Id == ExistentLotId);
        var result = new Promore.Api.Services.LotService(_lotRepository, _regionRepository, _clientRepository, _userRepository).DeleteAsync(ExistentLotId).Result;
        var deleted = !_context.Lots.Exists(x => x.Id == ExistentLotId);
        
        Assert.IsTrue(lotExists && result.Success && deleted);
    }

    [TestMethod]
    [TestCategory("Get")]
    public void GetLot_ById_ReturnsNotNull()
    {
        var result = new Promore.Api.Services.LotService(_lotRepository, _regionRepository, _clientRepository, _userRepository).GetByIdAsync(ExistentLotId).Result.Value;
        
        Assert.IsNotNull(result);
    }

    [TestMethod]
    [TestCategory("Get")]
    public void GetLotStatus_ByRegion_ReturnsNotNull()
    {
        var existentLotsInRegion = _context!.Lots.Where(x => x.RegionId == ExistentRegionId).ToList().Count;
        
        var result = new Promore.Api.Services.LotService(_lotRepository, _regionRepository, _clientRepository, _userRepository).GetStatusByRegionAsync(ExistentRegionId).Result;

        Assert.AreEqual(existentLotsInRegion, result.Value.Count);
    }

    [TestMethod]
    [TestCategory("Update")]
    public void Update_Lot_ReturnsTrue()
    {
        var existentLotStatus = _context!.Lots.FirstOrDefault(x => x.Id == ExistentLotId)!.Status;
        var model = BuildUpdateLotRequest(ExistentLotId, newStatus: existentLotStatus + 1);
        
        var result = new Promore.Api.Services.LotService(_lotRepository, _regionRepository, _clientRepository, _userRepository).UpdateAsync(model).Result;
        var updatedLotStatus = _context.Lots.FirstOrDefault(x => x.Id == ExistentLotId)!.Status;
        
        Assert.IsTrue(result.Success && updatedLotStatus != existentLotStatus);
    }

    private static CreateLotRequest BuildCreateLotRequest(string notExistentLotId, int existentUserId, int existentRegionId)
    {
        return new CreateLotRequest
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
    
    private static UpdateLotRequest BuildUpdateLotRequest(string existentLotId, int newStatus)
    {
        return new UpdateLotRequest
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