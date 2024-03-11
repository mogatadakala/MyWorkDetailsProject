using Microsoft.EntityFrameworkCore;

namespace MyWorkDetailsProject.Models
{
    public class ApplicationDBCOntext:DbContext
    {
        public ApplicationDBCOntext(DbContextOptions<ApplicationDBCOntext> options ) :base(options)
        { 
        }
        public DbSet<Login> logins { get; set; }
        public DbSet<Register> registers { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Login>().ToTable("Login").HasKey(lg => new
            {
                lg.UserID
            }); 
            modelBuilder.Entity<Register>().ToTable("Register").HasKey(rg => new
            {
                rg.UserID
            });
            base.OnModelCreating(modelBuilder);
        }
    }
}
