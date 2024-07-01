using Promore.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.AddConfigurationKeys();
builder.AddDatabase();
builder.AddJwtAuthentication();
builder.AddAuthorizationPolicies();
builder.JsonIgnoreCycles();
builder.AddDependencies();
builder.AddDocumentation();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.InitiateEmptyDataBase();

app.Run();
