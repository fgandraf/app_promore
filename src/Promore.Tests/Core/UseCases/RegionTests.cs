using Promore.Infra.Repositories.Mock;

namespace Promore.Tests.Core.UseCases;

[TestClass]
public class RegionTests
{
    private MockContext _context;
    private RegionRepositoryMock _regionRepository;
    private UserRepositoryMock _userRepository;
    
    private const int ExistentRegiontId = 1;
    
    [TestInitialize]
    public void TestInitialize()
    {
        _context = new MockContext();
        _regionRepository = new RegionRepositoryMock(_context);
        _userRepository = new UserRepositoryMock(_context);
    }

    [TestMethod]
    [TestCategory("Create")]
    public void Create_Region_ReturnsTrue()
    {
        var model = BuildCreateRegionRequest();
        
        var regionNotExists = !_context.Regions.Exists(x => x.Name == model.Name);
        var result = new Promore.Core.Contexts.RegionContext.UseCases.Create.Handler(_regionRepository, _userRepository).Handle(model).Result;
        var created = _context.Regions.Exists(x => x.Id == result.Value);
        
        Assert.IsTrue(regionNotExists && result.Success && created);
    }
    
    [TestMethod]
    public void Delete_Region_ReturnsTrue()
    {
        var regionExists = _context.Regions.Exists(x => x.Id == ExistentRegiontId);
        var result = new Promore.Core.Contexts.RegionContext.UseCases.Delete.Handler(_regionRepository).Handle(ExistentRegiontId).Result;
        var deleted = !_context.Regions.Exists(x => x.Id == ExistentRegiontId);
        
        Assert.IsTrue(regionExists && result.Success && deleted);
    }
    
    [TestMethod]
    [TestCategory("Get")]
    public void GetAll_Regions_SameCountThanMock()
    {
        var existentRegions = _context.Regions.Count;
        
        var result = new Promore.Core.Contexts.RegionContext.UseCases.GetAll.Handler(_regionRepository).Handle().Result.Value;
        
        Assert.AreEqual(existentRegions, result.Count);
    }
    
    [TestMethod]
    [TestCategory("Get")]
    public void GetRegion_ById_ReturnsNotNull()
    {
        var result = new Promore.Core.Contexts.RegionContext.UseCases.GetById.Handler(_regionRepository).Handle(ExistentRegiontId).Result.Value;
        
        Assert.IsNotNull(result);
    }
    
    [TestMethod]
    [TestCategory("Update")]
    public void Update_Region_ReturnsTrue()
    {
        var existentRegionName = _context.Regions.FirstOrDefault(x => x.Id == ExistentRegiontId)!.Name;
        var model = BuildUpdateRegionRequest(ExistentRegiontId);
        
        var result = new Promore.Core.Contexts.RegionContext.UseCases.Update.Handler(_regionRepository).Handle(model).Result;
        var updatedRegionName = _context.Regions.FirstOrDefault(x => x.Id == ExistentRegiontId)!.Name;
        
        Assert.IsTrue(result.Success && updatedRegionName != existentRegionName);
    }
    
    
    private static Promore.Core.Contexts.RegionContext.UseCases.Create.CreateRegionRequest BuildCreateRegionRequest()
    {
        return new Promore.Core.Contexts.RegionContext.UseCases.Create.CreateRegionRequest
        {
            Name = "Nova Região",
            EstablishedDate = new DateTime(2023, 12, 25),
            StartDate = new DateTime(2024, 01, 12),
            EndDate = new DateTime(2024, 06, 20),
            Users = []
        };
    }
    
    private static Promore.Core.Contexts.RegionContext.UseCases.Update.UpdateRegionRequest BuildUpdateRegionRequest(int existentRegionId)
    {
        return new Promore.Core.Contexts.RegionContext.UseCases.Update.UpdateRegionRequest()
        {
            Id = existentRegionId,
            Name = "Região atualizada",
            EstablishedDate = new DateTime(2023, 12, 25),
            StartDate = new DateTime(2024, 01, 12),
            EndDate = new DateTime(2024, 06, 20)
        };
    }
}