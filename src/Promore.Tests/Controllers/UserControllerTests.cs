using Promore.Api.Controllers;
using Promore.Api.Data.Contexts;
using Promore.Api.Handlers;
using Promore.Api.Services;

namespace Promore.Tests.Controllers;

public class UserControllerTests
{
    private readonly UserController _controller;

    public UserControllerTests()
    {
         var mockContext = new MockDataContext().Context;
        var handler = new UserHandler(mockContext.Object);
        _controller = new UserController(handler, new TokenService());
    }
    
}