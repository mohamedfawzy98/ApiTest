using ApiTest.Data.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ApiTest.Data.Context
{
    public class ApplicationContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options):base(options)
        {
            
        }
        public DbSet<Department> Departments { get; set; }  
        public DbSet<Employee> Employees { get; set; }  
    }
}
