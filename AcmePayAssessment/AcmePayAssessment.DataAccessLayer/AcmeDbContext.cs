using AcmePayAssessment.Entity;
using Microsoft.EntityFrameworkCore;

namespace AcmePayAssessment.DataAccessLayer
{
    public class AcmeDbContext : DbContext
    {
        public AcmeDbContext(DbContextOptions<AcmeDbContext> options) : base(options) { }

        public DbSet<Transactions> Transactions { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            foreach (var entityType in builder.Model.GetEntityTypes())
            {
                var tableName = entityType.GetTableName();
                if (tableName.StartsWith("AspNet"))
                {
                    entityType.SetTableName(tableName.Substring(6));
                    entityType.SetSchema("auth");
                }
            }

            builder.Entity<Transactions>().Property(x => x.OrderReference).HasMaxLength(50);
            builder.Entity<Transactions>().Property(x => x.Currency).HasMaxLength(3);

        }

    }
}
