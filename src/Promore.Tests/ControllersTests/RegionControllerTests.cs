using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Promore.Api.Controllers;
using Promore.Api.Data.Contexts;
using Promore.Api.Handlers;
using Promore.Core.Responses.Regions;

namespace Promore.Tests.ControllersTests;

public class RegionControllerTests
{
    private readonly RegionController _controller;
    
    private const int AmountOfRegions = 2;

    public RegionControllerTests()
    {
        var mockContext = new MockDataContext().Context;
        var handler = new RegionHandler(mockContext.Object);
        _controller = new RegionController(handler);
    }
    
    [Fact]
    public void GetAll_ReturnsHttpStatusCode200()
    {
        // Act
        var result = _controller.GetAll();
        
        // Assert
        var statusCode = result switch
        {
            ObjectResult objectResult => objectResult.StatusCode,
            StatusCodeResult statusCodeResult => statusCodeResult.StatusCode,
            _ => null
        };
        
        Assert.Equal(200, statusCode);
    }
    
    
    [Fact]
    public void GetAll_ReturnsExpectedNumberOfRegions()
    {
        // Act
        
        var result = (ObjectResult)_controller.GetAll();
        var jsonResult = JsonSerializer.Serialize(result.Value);
        var jsonDoc = JsonDocument.Parse(jsonResult);
        var data = jsonDoc.RootElement.GetProperty("Data").GetRawText();
        var clients = JsonSerializer.Deserialize<List<GetRegionsResponse>>(data);
        
        // Assert
        
        Assert.Equal(AmountOfRegions, clients!.Count);
    }
    
}