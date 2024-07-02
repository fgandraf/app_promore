using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Promore.Api.Controllers;
using Promore.Api.Data.Contexts;
using Promore.Api.Handlers;
using Promore.Core.Responses.Lots;

namespace Promore.Tests.ControllersTests;

public class LotControllerTests
{
    private readonly LotController _controller;
    
    private const int AmountOfLotsInRegion1 = 3;
    private const int AmountOfLotsInRegion2 = 2;

    public LotControllerTests()
    {
        var mockContext = new MockDataContext().Context;
        var handler = new LotHandler(mockContext.Object);
        _controller = new LotController(handler);
    }
    
    
    [Fact]
    public void GetAll_ReturnsHttpStatusCode200()
    {
        // Act
        var result = _controller.GetStatusByRegion(1);
        
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
    public void GetStatusByRegion_ReturnsExpectedNumberOfLotsInRegion1()
    {
        // Act
        
        var result = (ObjectResult)_controller.GetStatusByRegion(1);
        var jsonResult = JsonSerializer.Serialize(result.Value);
        var jsonDoc = JsonDocument.Parse(jsonResult);
        var data = jsonDoc.RootElement.GetProperty("Data").GetRawText();
        var lots = JsonSerializer.Deserialize<List<LotsStatusResponse>>(data);
        
        // Assert
        
        Assert.Equal(AmountOfLotsInRegion1, lots!.Count);
    }
    
    [Fact]
    public void GetStatusByRegion_ReturnsExpectedNumberOfLotsInRegion2()
    {
        // Act
        
        var result = (ObjectResult)_controller.GetStatusByRegion(2);
        var jsonResult = JsonSerializer.Serialize(result.Value);
        var jsonDoc = JsonDocument.Parse(jsonResult);
        var data = jsonDoc.RootElement.GetProperty("Data").GetRawText();
        var lots = JsonSerializer.Deserialize<List<LotsStatusResponse>>(data);
        
        // Assert
        
        Assert.Equal(AmountOfLotsInRegion2, lots!.Count);
    }
    
}