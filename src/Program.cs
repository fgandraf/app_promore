using PromoreApi.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var dbContext = new PromoreDataContext(builder.Configuration.GetConnectionString("Default"));

//builder.Services.AddScoped<Interface, Implementation>();

var app = builder.Build();

app.MapControllers();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.Run();