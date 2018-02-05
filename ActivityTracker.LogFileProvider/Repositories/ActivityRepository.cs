using System;
using System.Collections.Generic;

namespace ActivityTracker.LogFileProvider
{
    public class ActivityRepository : IActivityRepository
    {
        public void Add(IActivity activity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IActivity> GetActivities()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IActivity> GetActivities(Func<IActivity, bool> predicate)
        {
            throw new NotImplementedException();
        }
    }
}