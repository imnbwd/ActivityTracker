using System.Data.Entity;

namespace ActivityTracker.SqlServerProvider
{
    public class ActivityDbContext : DbContext
    {
        public DbSet<Activity> Activities { get; set; }

        public DbSet<OperationDuration> OperationDurations { get; set; }
    }
}