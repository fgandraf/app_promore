using System.Text.Json.Serialization;
using PromoreApi;
using PromoreApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Registers controller services
builder.Services.AddControllers().AddJsonOptions(options => 
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

// Extends Services to add JWT Bearer authentication with token validation
builder.Services.AddBearerAuthentication(builder.Configuration);

// Extends Services to add Dependency Injection services configuration
builder.Services.AddDatabaseServices();
builder.Services.AddRepositoryServices();

builder.Services.AddSwaggerConfiguration();

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