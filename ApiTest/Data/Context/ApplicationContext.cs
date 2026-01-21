using ApiTest.Data.Model;
using Microsoft.EntityFrameworkCore;

namespace ApiTest.Data.Context
{
    public class ApplicationContext :DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options):base(options)
        {
                
        }

        public DbSet<Department> Departments { get; set; }  
        public DbSet<Employee> Employees { get; set; }  
    }
}
