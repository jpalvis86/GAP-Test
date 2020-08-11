using Insurance.Repository.Entities;
using Microsoft.EntityFrameworkCore;

namespace Insurance.Repository
{
    public class InsuranceDbContext : DbContext
    {
        public DbSet<InsuranceEntity> Insurances;

        public InsuranceDbContext(DbContextOptions<InsuranceDbContext> options) : base(options)
        {
            // TODO: Seed data here...
        }
    }
}
