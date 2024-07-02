using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Promore.Api.Controllers;
using Promore.Api.Data.Contexts;
using Promore.Api.Handlers;
using Promore.Core.Responses.Clients;

namespace Promore.Tests.ControllersTests;

public class ClientControllerTests
{
    private readonly ClientController _controller;
    
    private const int AmountOfClients = 5;

    public ClientControllerTests()
    {
        // Arrange
        var mockContext = new MockDataContext().Context;
        var handler = new ClientHandler(mockContext.Object);
        _controller = new ClientController(handler);
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
    public void GetAll_ReturnsExpectedNumberOfClients()
    {
        // Act
        
        var result = (ObjectResult)_controller.GetAll();
        var jsonResult = JsonSerializer.Serialize(result.Value);
        var jsonDoc = JsonDocument.Parse(jsonResult);
        var data = jsonDoc.RootElement.GetProperty("Data").GetRawText();
        var clients = JsonSerializer.Deserialize<List<ClientResponse>>(data);
        
        // Assert
        
        Assert.Equal(AmountOfClients, clients!.Count);
    }
    
}