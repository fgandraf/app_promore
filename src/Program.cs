using System.Text.Json.Serialization;
using PromoreApi;
using PromoreApi.Extensions;


var builder = WebApplication.CreateBuilder(args);

// Get private JwtKey from appsettings.json
Configuration.JwtKey = builder.Configuration.GetValue<string>("JwtKey");

// Extends Services to add authentication and authorization
builder.Services.AddBearerAuthentication();
builder.Services.AddAuthorizationPolicies();

// Registers controller services
builder.Services.AddControllers().AddJsonOptions(options => 
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

// Extends Services to add dependency injection services configurations
builder.Services.AddDatabaseServices(builder.Configuration);
builder.Services.AddRepositoryServices();

// Extends Services to add swagger configurations
builder.Services.AddSwaggerConfigurations();

// Configures API Explorer to generate metadata for API endpoints
builder.Services.AddEndpointsApiExplorer();


var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// First run - including data do Database
using var scope = app.Services.CreateScope();
var dbInserts = scope.ServiceProvider.GetRequiredService<DbInserts>();
dbInserts.InsertData();


app.Run();