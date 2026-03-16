using System.Data.Entity;

namespace OrganizationMVC.Models
{
    public class OrganizationDbContext : DbContext
    {
        public OrganizationDbContext() : base("name=OrganizationDbConnection")
        {
        }

        public DbSet<Department> Departments { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
