using Promore.Api.Controllers;
using Promore.Api.Data.Contexts;
using Promore.Api.Handlers;

namespace Promore.Tests.Controllers;

public class RegionControllerTests
{
    private readonly RegionController _controller;

    public RegionControllerTests()
    {
        var mockContext = new MockDataContext().Context;
        var handler = new RegionHandler(mockContext.Object);
        _controller = new RegionController(handler);
    }
    
}