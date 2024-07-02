using Promore.Api.Controllers;
using Promore.Api.Data.Contexts;
using Promore.Api.Handlers;

namespace Promore.Tests.Controllers;

public class LotControllerTests
{
    private readonly LotController _controller;

    public LotControllerTests()
    {
        var mockContext = new MockDataContext().Context;
        var handler = new LotHandler(mockContext.Object);
        _controller = new LotController(handler);
    }
    
    
}