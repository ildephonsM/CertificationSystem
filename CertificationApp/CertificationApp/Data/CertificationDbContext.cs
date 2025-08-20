using CertificationApp.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace CertificationApp.Data
{
    public class CertificationDbContext : DbContext
    {
        public CertificationDbContext(DbContextOptions<CertificationDbContext> options)
           : base(options) { }

        public DbSet<CreateUser> Users { get; set; } = default!;
        public DbSet<Courses> Courses { get; set; } = default!;
    }
}
