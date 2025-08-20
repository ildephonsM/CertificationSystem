using Microsoft.EntityFrameworkCore;
using CertificationApp.Shared.Models;

namespace CertificationApp.Data
{
    public class CertificationDbContext : DbContext
    {
        public CertificationDbContext(DbContextOptions<CertificationDbContext> options)
           : base(options) { }

        public DbSet<CreateUser> Users { get; set; } = default!;
    }
}
