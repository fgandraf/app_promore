using Microsoft.EntityFrameworkCore;
using PromoreApi.Data;

namespace PromoreApi.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddDatabaseServices(this IServiceCollection services)
    {
        services.AddDbContext<PromoreDataContext>(options =>
            options.UseSqlServer("Server=localhost,1433;Database=Promore;User ID=sa;Password=1q2w3e4r@#$;Encrypt=false"));
        return services;
    }
    
    public static IServiceCollection AddRepositoryServices(this IServiceCollection services)
    {
        services.AddScoped<DbInserts>();
        services.AddScoped<PromoreDataContext>();
        return services;
    }
}
