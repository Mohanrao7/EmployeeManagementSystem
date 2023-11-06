
using EmployeeManagementApi.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementApi.Data
{
    public class EmployeeDbContext: DbContext
    {
        public EmployeeDbContext(DbContextOptions<EmployeeDbContext> dbContextOptions): base(dbContextOptions)
        {
            
        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<EmployeeDepartmentAssociation> employeeDepartmentAssociations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
                .ToTable("Employees")
                .HasKey(e => e.Id);

            

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Department>()
                .ToTable("Departments")
                .HasKey(e => e.Id);
            base.OnModelCreating(modelBuilder);
        }
    }
}
