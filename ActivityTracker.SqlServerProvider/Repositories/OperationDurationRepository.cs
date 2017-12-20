namespace ActivityTracker.SqlServerProvider
{
    public class OperationDurationRepository : IOperationDurationRepository
    {
        public void Add(IOperationDuration operationDuration)
        {
            if (operationDuration == null)
            {
                throw new System.ArgumentNullException(nameof(operationDuration));
            }

            using (ActivityDbContext db = new ActivityDbContext())
            {
                var durationEntity = new OperationDuration
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