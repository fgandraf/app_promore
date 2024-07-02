using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Promore.Api.Controllers;
using Promore.Api.Data.Contexts;
using Promore.Api.Handlers;
using Promore.Api.Services;
using Promore.Core.Responses.Users;

namespace Promore.Tests.ControllersTests;

public class UserControllerTests
{
    private readonly UserController _controller;
    
    private const int AmountOfUsers = 4;

    public UserControllerTests()
    {
         var mockContext = new MockDataContext().Context;
        var handler = new UserHandler(mockContext.Object);
        _controller = new UserController(handler, new TokenService());
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
    public void GetAll_ReturnsExpectedNumberOfUsers()
    {
        // Act
        
        var result = (ObjectResult)_controller.GetAll();
        var jsonResult = JsonSerializer.Serialize(result.Value);
        var jsonDoc = JsonDocument.Parse(jsonResult);
        var data = jsonDoc.RootElement.GetProperty("Data").GetRawText();
        var clients = JsonSerializer.Deserialize<List<GetUserResponse>>(data);
        
        // Assert
        
        Assert.Equal(AmountOfUsers, clients!.Count);
    }
    
}