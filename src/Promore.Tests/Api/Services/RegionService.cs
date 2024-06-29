using Promore.Api.Services;
using Promore.Core.Requests.Regions;
using Promore.Infra.Repositories.Mock;

namespace Promore.Tests.Api.Services;

[TestClass]
public class RegionService
{
    private MockContext? _context;
    private RegionHandlerMock? _regionRepository;
    private UserHandlerMock? _userRepository;
    
    private const int ExistentRegiontId = 1;
    
    [TestInitialize]
    public void TestInitialize()
    {
        _context = new MockContext();
        _regionRepository = new RegionHandlerMock(_context);
        _userRepository = new UserHandlerMock(_context);
    }

    [TestMethod]
    [TestCategory("Create")]
    public void Create_Region_ReturnsTrue()
    {
        var model = BuildCreateRegionRequest();
        
        var regionNotExists = !_context!.Regions.Exists(x => x.Name == model.Name);
        var result = new Promore.Api.Services.RegionService(_regionRepository, _userRepository).CreateAsync(model).Result;
        var created = _context!.Regions.Exists(x => x.Id == result.Value);
        
        Assert.IsTrue(regionNotExists && result.Success && created);
    }
    
    [TestMethod]
    public void Delete_Region_ReturnsTrue()
    {
        var regionExists = _context!.Regions.Exists(x => x.Id == ExistentRegiontId);
        var result = new Promore.Api.Services.RegionService(_regionRepository, _userRepository).DeleteAsync(ExistentRegiontId).Result;
        var deleted = !_context.Regions.Exists(x => x.Id == ExistentRegiontId);
        
        Assert.IsTrue(regionExists && result.Success && deleted);
    }
    
    [TestMethod]
    [TestCategory("Get")]
    public void GetAll_Regions_SameCountThanMock()
    {
        var existentRegions = _context!.Regions.Count;
        
        var result = new Promore.Api.Services.RegionService(_regionRepository, _userRepository).GetAllAsync().Result.Value;
        
        Assert.AreEqual(existentRegions, result.Count);
    }
    
    [TestMethod]
    [TestCategory("Get")]
    public void GetRegion_ById_ReturnsNotNull()
    {
        var result = new Promore.Api.Services.RegionService(_regionRepository, _userRepository).GetByIdAsync(ExistentRegiontId).Result.Value;
        
        Assert.IsNotNull(result);
    }
    
    [TestMethod]
    [TestCategory("Update")]
    public void Update_Region_ReturnsTrue()
    {
        var existentRegionName = _context!.Regions.FirstOrDefault(x => x.Id == ExistentRegiontId)!.Name;
        var model = BuildUpdateRegionRequest(ExistentRegiontId);
        
        var result = new Promore.Api.Services.RegionService(_regionRepository, _userRepository).UpdateAsync(model).Result;
        var updatedRegionName = _context.Regions.FirstOrDefault(x => x.Id == ExistentRegiontId)!.Name;
        
        Assert.IsTrue(result.Success && updatedRegionName != existentRegionName);
    }
    
    
    private static CreateRegionRequest BuildCreateRegionRequest()
    {
        return new CreateRegionRequest
        {
            Name = "Nova Região",
            EstablishedDate = new DateTime(2023, 12, 25),
            StartDate = new DateTime(2024, 01, 12),
            EndDate = new DateTime(2024, 06, 20),
            Users = []
        };
    }
    
    private static UpdateRegionRequest BuildUpdateRegionRequest(int existentRegionId)
    {
        return new UpdateRegionRequest()
        {
            Id = existentRegionId,
            Name = "Região atualizada",
            EstablishedDate = new DateTime(2023, 12, 25),
            StartDate = new DateTime(2024, 01, 12),
            EndDate = new DateTime(2024, 06, 20)
        };
    }
}