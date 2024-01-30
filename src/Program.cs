using System.Text.Json;
using System.Text.Json.Serialization;
using PromoreApi;
using PromoreApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDatabaseServices();
builder.Services.AddRepositoryServices();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

var app = builder.Build();

using var scope = app.Services.CreateScope();
var dbInserts = scope.ServiceProvider.GetRequiredService<DbInserts>();
dbInserts.InsertData();

app.MapControllers();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.Run();