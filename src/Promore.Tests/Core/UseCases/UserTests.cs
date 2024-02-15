using Promore.Core.Contexts.UserContext.UseCases.RemoveLotFromUser;
using Promore.Infra.Repositories.Mock;

namespace Promore.Tests.Core.UseCases;

[TestClass]
public class UserTests
{
    private MockContext _context;
    private UserRepositoryMock _userRepository;
    private RegionRepositoryMock _regionRepository;
    private RoleRepositoryMock _roleRepository;
    
    private const string ExistentLotId = "A10";
    private const string ExistentEmail = "fgandraf@gmail.com";
    private const int ExistentUserId = 2;
    private const string NotExistentEmail = "test@email.com";
    
    [TestInitialize]
    public void TestInitialize()
    {
        _context = new MockContext();
        _userRepository = new UserRepositoryMock(_context);
        _regionRepository = new RegionRepositoryMock(_context);
        _roleRepository = new RoleRepositoryMock(_context);
    }
    
    [TestMethod]
    [TestCategory("Create")]
    public void Create_User_ReturnsTrue()
    {
        var model = BuildCreateUserRequest(NotExistentEmail);
        
        var userNotExists = !_context.Users.Exists(x => x.Email == model.Email);
        var result = new Promore.Core.Contexts.UserContext.UseCases.Create.Handler(_userRepository,_regionRepository,_roleRepository).Handle(model).Result;
        var created = _context.Users.Exists(x => x.Id == result.Value);
        
        Assert.IsTrue(userNotExists && result.Success && created);
    }
    
    [TestMethod]
    public void RemoveLot_User_ReturnsTrue()
    {
        var userAndLotExists = _context
            .Users
            .FirstOrDefault(x => x.Id == ExistentUserId)!
            .Lots
            .Contains(_context.Lots.FirstOrDefault(x => x.Id == ExistentLotId));
        
        var result = new Handler(_userRepository).Handle(ExistentUserId, ExistentLotId).Result;
        
        var deleted = !_context
            .Users
            .FirstOrDefault(x => x.Id == ExistentUserId)!
            .Lots
            .Contains(_context.Lots.FirstOrDefault(x => x.Id == ExistentLotId));
        
        Assert.IsTrue(userAndLotExists && result.Success && deleted);
    }
    
    [TestMethod]
    [TestCategory("Get")]
    public void GetAll_Users_SameCountThanMock()
    {
        var existentUsers = _context.Users.Count;
        
        var result = new Promore.Core.Contexts.UserContext.UseCases.GetAll.Handler(_userRepository).Handle().Result.Value;
        
        Assert.AreEqual(existentUsers, result.Count);
    }
    
    [TestMethod]
    [TestCategory("Get")]
    public void GetUser_ByEmail_ReturnsNotNull()
    {
        var result = new Promore.Core.Contexts.UserContext.UseCases.GetByEmail.Handler(_userRepository).Handle(ExistentEmail).Result.Value;
        
        Assert.IsNotNull(result);
    }
    
    [TestMethod]
    [TestCategory("Get")]
    public void GetUser_ById_ReturnsNotNull()
    {
        var result = new Promore.Core.Contexts.UserContext.UseCases.GetById.Handler(_userRepository).Handle(ExistentUserId).Result.Value;
        
        Assert.IsNotNull(result);
    }

    [TestMethod]
    [TestCategory("Get")]
    public void GetUser_ByLogin_ReturnsNotNull()
    {
        var model = BuildLoginRequest();
        
        var result = new Promore.Core.Contexts.UserContext.UseCases.GetByLogin.Handler(_userRepository).Handle(model).Result.Value;
        
        Assert.IsNotNull(result);
    }
    
    [TestMethod]
    [TestCategory("Update")]
    public void Update_UserInfo_ReturnsTrue()
    {
        var existentUserEmail = _context.Users.FirstOrDefault(x => x.Id == ExistentUserId)!.Email;
        var model = BuildUpdateUserInfoRequest(ExistentUserId, NotExistentEmail);
        
        var result = new Promore.Core.Contexts.UserContext.UseCases.UpdateInfo.Handler(_userRepository).Handle(model).Result;
        var updatedUserEmail = _context.Users.FirstOrDefault(x => x.Id == ExistentUserId)!.Email;
        
        Assert.IsTrue(result.Success && updatedUserEmail != existentUserEmail);
    }


    [TestMethod]
    [TestCategory("Update")]
    public void Update_UserSettings_ReturnsTrue()
    {
        var existentUserActive = _context.Users.FirstOrDefault(x => x.Id == ExistentUserId)!.Active;
        var model = BuildUpdateUserSettingsRequest(ExistentUserId);
        
        var result = new Promore.Core.Contexts.UserContext.UseCases.UpdateSettings.Handler(_userRepository, _regionRepository, _roleRepository).Handle(model).Result;
        var updatedUserActive = _context.Users.FirstOrDefault(x => x.Id == ExistentUserId)!.Active;
        
        Assert.IsTrue(result.Success && updatedUserActive != existentUserActive);
    }
    
    private static Promore.Core.Contexts.UserContext.UseCases.Create.CreateUserRequest BuildCreateUserRequest(string notExistentEmail)
    {
        return new Promore.Core.Contexts.UserContext.UseCases.Create.CreateUserRequest
        {
            Active = true,
            Email = notExistentEmail,
            Password = "123senhaABC",
            Name = "Novo Usuário",
            Cpf = "00011122233",
            Profession = "Engenheiro Civil",
            Roles = [2,3],
            Regions = [1,2]
        };
    }
    
    private static Promore.Core.Contexts.UserContext.UseCases.UpdateInfo.UpdateUserInfoRequest BuildUpdateUserInfoRequest(int id, string notExistentEmail)
    {
        return new Promore.Core.Contexts.UserContext.UseCases.UpdateInfo.UpdateUserInfoRequest
        {
            Id = id,
            Email = notExistentEmail,
            Password = "123senhaABC",
            Name = "Novo Usuário",
            Cpf = "00011122233",
            Profession = "Engenheiro Civil"
        };
    }
    
    private static Promore.Core.Contexts.UserContext.UseCases.UpdateSettings.UpdateUserSettingsRequest BuildUpdateUserSettingsRequest(int id)
    {
        return new Promore.Core.Contexts.UserContext.UseCases.UpdateSettings.UpdateUserSettingsRequest
        {
            Id = id,
            Active = false,
            Roles = [2,3],
            Regions = [1,2]
        };
    }
    
    private static Promore.Core.Contexts.UserContext.UseCases.GetByLogin.LoginRequest BuildLoginRequest()
    {
        return new Promore.Core.Contexts.UserContext.UseCases.GetByLogin.LoginRequest
        {
            Email = "fgandraf@gmail.com",
            Password = "12345senha"
        };
    }
}