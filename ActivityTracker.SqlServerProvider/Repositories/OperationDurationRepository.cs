namespace ActivityTracker.SqlServerProvider.Repositories
{
    public class OperationDurationRepository : IOperationDurationRepository
    {
        public void Add(IOperationDuration operationDuration)
        {
            using (ActivityDbContext db = new ActivityDbContext())
            {
                var durationEntity = new Entities.OperationDuration
                {
                    Action = operationDuration.Action,
                    Level = operationDuration.Level,
                    Duration = operationDuration.Duration
                };

                db.OperationDurations.Add(durationEntity);

                db.SaveChanges();
            }
        }
    }
}