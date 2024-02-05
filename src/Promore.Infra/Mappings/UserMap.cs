using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Promore.Core.Contexts.Region.Entity;
using Promore.Core.Contexts.Role.Entity;
using Promore.Core.Contexts.User.Entity;

namespace Promore.Infra.Mappings;

public class UserMap : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        
        //// Table
        builder.ToTable("User");

        
        //// Primary Key - Identity
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd()
            .UseIdentityColumn();
        
        
        //// Properties
        builder.Property(x => x.Active)
            .IsRequired()
            .HasColumnName("Active")
            .HasColumnType("BIT");

        builder.Property(x => x.Email)
            .IsRequired()
            .HasColumnName("Email")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(50)
            .HasAnnotation("EmailAddress", true);
            
        builder.Property(x => x.PasswordHash)
            .IsRequired()
            .HasColumnName("PasswordHash")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(255);
        
        builder.Property(x => x.Name)
            .IsRequired()
            .HasColumnName("Name")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(100);
        
        builder.Property(x => x.Cpf)
            .IsRequired()
            .HasColumnName("Cpf")
            .HasColumnType("VARCHAR")
            .HasMaxLength(11);
        
        builder.Property(x => x.Profession)
            .IsRequired(false)
            .HasColumnName("Profession")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(50);
        
        
        //// Index
        builder.HasIndex(x => x.Email, "IX_User_Email")
            .IsUnique();
        
        
        //// Relation
        // User <-> Role
        builder.HasMany(x => x.Roles)
            .WithMany(x => x.Users)
            .UsingEntity<Dictionary<string, object>>
            (
                "UserRole",
                user => user.HasOne<Role>()
                    .WithMany()
                    .HasForeignKey("RoleId")
                    .HasConstraintName("FK_UserRole_RoleId"),
                role => role.HasOne<User>()
                    .WithMany()
                    .HasForeignKey("UserId")
                    .HasConstraintName("FK_UserRole_UserId")
            );
        
        // User <-> Region
        builder.HasMany(x => x.Regions)
            .WithMany(x => x.Users)
            .UsingEntity<Dictionary<string, object>>
            (
                "UserRegion",
                user => user.HasOne<Region>()
                    .WithMany()
                    .HasForeignKey("RegionId")
                    .HasConstraintName("FK_UserRegion_RegionId"),
                region => region.HasOne<User>()
                    .WithMany()
                    .HasForeignKey("UserId")
                    .HasConstraintName("FK_UserRegion_UserId")
            );
        
        // User <- Professional
        
    }
}