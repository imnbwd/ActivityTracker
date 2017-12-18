using System.Data.Entity;

namespace ActivityTracker.SqlServerProvider
{
    public class ActivityDbContext : DbContext
    {
        public DbSet<Entities.Activity> Activities { get; set; }

        public DbSet<Entities.OperationDuration> OperationDurations { get; set; }
    }
}