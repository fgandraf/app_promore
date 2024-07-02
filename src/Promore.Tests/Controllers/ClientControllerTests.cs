using Microsoft.EntityFrameworkCore;
using Promore.Api.Controllers;
using Promore.Api.Data.Contexts;
using Promore.Api.Handlers;

namespace Promore.Tests.Controllers;

public class ClientControllerTests
{
    private readonly ClientController _controller;

    public ClientControllerTests()
    {
        var mockContext = new MockDataContext().Context;
        var handler = new ClientHandler(mockContext.Object);
        _controller = new ClientController(handler);
    }
    
    
}