using System.Text;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Promore.Api.Data;
using Promore.Api.Data.Contexts;
using Promore.Api.Handlers;
using Promore.Api.Services;
using Promore.Core;
using Promore.Core.Data.Contexts;
using Promore.Core.Handlers;

namespace Promore.Api.Extensions;

public static class BuilderExtensions
{
    public static void AddConfigurationKeys(this WebApplicationBuilder builder)
    {
        Configuration.Database.ConnectionString = builder.Configuration.GetConnectionString("Default")!;
        Configuration.Secrets.JwtPrivateKey = builder.Configuration.GetSection("Secrets").GetValue<string>("JwtPrivateKey") ?? string.Empty;
        Configuration.IsMockDataBase = builder.Configuration.GetValue<bool>("MockDataSource");
    }

    public static void AddDatabase(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<PromoreDataContext>(options =>
            options.UseSqlServer(
                Configuration.Database.ConnectionString
                )
            );
    }
    
    public static void AddJwtAuthentication(this WebApplicationBuilder builder)
    {
        var key = Encoding.ASCII.GetBytes(Configuration.Secrets.JwtPrivateKey);
        
        builder.Services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(x =>
        {
            x.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false
            };
        });
    }
    
    public static void AddAuthorizationPolicies(this WebApplicationBuilder builder)
        => builder.Services.AddAuthorization(x =>
        {
            x.AddPolicy("admin", p => p.RequireRole("admin"));
            x.AddPolicy("manager", p => p.RequireRole("manager"));
        });
        
    public static void AddDependencies(this WebApplicationBuilder builder)
    {
        if (Configuration.IsMockDataBase)
            builder.Services.AddScoped<PromoreDataContext>(provider => new MockDataContext().Context.Object);
        else
            builder.Services.AddScoped<PromoreDataContext>();
        
        
        builder.Services.AddScoped<IUserHandler, UserHandler>();
        builder.Services.AddScoped<IRegionHandler, RegionHandler>();
        builder.Services.AddScoped<IClientHandler, ClientHandler>();
        builder.Services.AddScoped<ILotHandler, LotHandler>();
        builder.Services.AddScoped<IRoleHandler, RoleHandler>();
        builder.Services.AddScoped<TokenService>();
    }
    
    public static void AddDocumentation(this WebApplicationBuilder builder)
    {
        builder.Services.AddEndpointsApiExplorer();
        
        builder.Services.AddSwaggerGen(setup =>
        {
            setup.SwaggerDoc("v1", new OpenApiInfo { Title = "Promore API", Version = "v1" });
            
            var jwtSecurityScheme = new OpenApiSecurityScheme
            {
                BearerFormat = "JWT",
                Name = "JWT Authentication",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = JwtBearerDefaults.AuthenticationScheme,
                Description = "Put **_ONLY_** your JWT Bearer token on textbox below!",

                Reference = new OpenApiReference
                {
                    Id = JwtBearerDefaults.AuthenticationScheme,
                    Type = ReferenceType.SecurityScheme
                }
            };

            setup.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

            setup.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                { jwtSecurityScheme, Array.Empty<string>() }
            });
            
            setup.CustomSchemaIds(n => n.FullName);
            setup.EnableAnnotations();

        });
    }

    public static void JsonIgnoreCycles(this WebApplicationBuilder builder)
    {
        builder.Services.AddControllers().AddJsonOptions(options => 
            options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
    }
    
}