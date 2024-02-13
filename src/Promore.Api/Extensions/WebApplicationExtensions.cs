using Promore.Core.Contexts.RegionContext.Entities;
using Promore.Core.Contexts.RoleContext.Entities;
using Promore.Core.Contexts.UserContext.Entities;
using Promore.Infra.Data;
using SecureIdentity.Password;

namespace Promore.Api.Extensions;

public static class WebApplicationExtensions
{
    public static void InitiateEmptyDataBase(this WebApplication app)
    {
        var isMock = app.Configuration.GetValue<bool>("MockDataSource");

        if (isMock)
            return;
        
        using var scope = app.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<PromoreDataContext>();

        #region CreateRoles
        if (!context.Roles.Any())
        {
            context.Roles.Add(new Role { Name = "admin" });
            context.Roles.Add(new Role { Name = "professional" });
            context.Roles.Add(new Role { Name = "manager" });
            context.SaveChanges();
        }
        #endregion
        
        #region CreateRegion
        if (!context.Regions.Any())
        {
            context.Regions.Add(new Region { Name = "Vila do Sucesso - Bauru/SP", EstablishedDate = new DateTime(2023, 12, 12), StartDate = new DateTime(2023, 02, 15), EndDate = new DateTime(2023, 12, 31)  });
            context.SaveChanges();
        }
        #endregion

        #region CreateUserAdmin
        if (!context.Users.Any())
        {
            var admin = new User
            {
                Active = true,
                Email = "admin@admin",
                Name = "Administrador",
                Cpf = "",
                PasswordHash = PasswordHasher.Hash("admin"),
                Roles =
                [
                    context.Roles.FirstOrDefault(x => x.Name == "Admin")
                ]
            };
            
            context.Users.Add(admin);
            context.SaveChanges();
        }
        #endregion
    }
}