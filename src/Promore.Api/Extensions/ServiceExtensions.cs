using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Promore.Api.Data;
using Promore.Api.Repositories.Contracts;
using Promore.Api.Repositories.Database;
using Promore.Api.Services;

namespace Promore.Api.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddDatabaseServices(this IServiceCollection services, IConfiguration configuration)
        => services.AddDbContext<PromoreDataContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("Default")));
    
    public static IServiceCollection AddRepositoryServices(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository,UserRepository>();
        services.AddScoped<IRegionRepository, RegionRepository>();
        services.AddScoped<IClientRepository, ClientRepository>();
        services.AddScoped<ILotRepository, LotRepository>();
        services.AddScoped<IAccountRepository, AccountRepository>();
        
        services.AddScoped<DbInserts>();
        services.AddScoped<PromoreDataContext>();
        services.AddTransient<TokenService>();
        return services;
    }
    
    public static IServiceCollection AddBearerAuthentication(this IServiceCollection services)
    {
        var key = Encoding.ASCII.GetBytes(Configuration.JwtKey);
        
        services.AddAuthentication(x =>
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
        
        return services;
    }

    public static IServiceCollection AddAuthorizationPolicies(this IServiceCollection services)
        => services.AddAuthorization(x =>
        {
            x.AddPolicy("admin", p => p.RequireRole("admin"));
            x.AddPolicy("manager", p => p.RequireRole("manager"));
        });
    
    
    public static IServiceCollection AddSwaggerConfigurations(this IServiceCollection services)
    {
        services.AddSwaggerGen(setup =>
        {
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

        });
        return services;
    }
}

