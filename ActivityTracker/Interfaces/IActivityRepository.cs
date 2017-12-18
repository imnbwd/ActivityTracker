using System;
using System.Collections.Generic;

namespace ActivityTracker
{
    public interface IActivityRepository
    {
        void Add(IActivity activity);

        IEnumerable<IActivity> GetActivities();

        IEnumerable<IActivity> GetActivities(Func<IActivity, bool> predicate);
    }
}