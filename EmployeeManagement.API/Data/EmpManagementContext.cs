using EmployeeManagement.API.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace EmployeeManagement.API.Data;

public class EmpManagementContext : IdentityDbContext
{
    public EmpManagementContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Employee> Employees { get; set; }
    public DbSet<Department> Deparments { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        foreach (var entityType in builder.Model.GetEntityTypes())
        {
            var tableName = entityType.GetTableName();
            if (tableName.StartsWith("AspNet"))
            {
                entityType.SetTableName(tableName.Substring(6));
            }
        }
        
        builder.Entity<IdentityRole>()
            .HasData(
                new IdentityRole {Id = "1", Name = "Member", NormalizedName = "MEMBER" },
                new IdentityRole {Id = "2", Name = "Admin", NormalizedName = "ADMIN" }
            );
    }
}