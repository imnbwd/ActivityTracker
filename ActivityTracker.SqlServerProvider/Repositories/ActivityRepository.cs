using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ActivityTracker.SqlServerProvider
{
    public class ActivityRepository : IActivityRepository
    {
        public void Add(IActivity activity)
        {
            if (activity == null)
            {
                throw new ArgumentNullException(nameof(activity));
            }

            using (ActivityDbContext db = new ActivityDbContext())
            {
                var activityEntity = new Activity
                {
                    Action = activity.Action,
                    Level = activity.Level,
                    Recipient = activity.Recipient,
                    Time = activity.Time,
                };

                if (activity.Values != null)
                {
                    try
                    {
                        activityEntity.RelatedData = JsonConvert.SerializeObject(activity.Values);
                    }
                    catch (Exception ex)
                    {
                        throw new ArgumentException("The property Values cannot be serialized into Json format", ex);
                    }
                }

                db.Activities.Add(activityEntity);

                db.SaveChanges();
            }
        }

        public IEnumerable<IActivity> GetActivities()
        {
            using (ActivityDbContext db = new ActivityDbContext())
            {
                var entityList = db.Activities.ToList();

                List<IActivity> activities = new List<IActivity>();

                foreach (var entity in entityList)
                {
                    var activity = new ActivityTracker.Activity
                    {
                        Action = entity.Action,
                        Level = entity.Level,
                        Recipient = entity.Recipient,
                        Time = entity.Time
                    };

                    var values = entity.RelatedData != null ? JsonConvert.DeserializeObject<Dictionary<string, object>>(entity.RelatedData) : new Dictionary<string, object>();

                    foreach (KeyValuePair<string, object> item in values)
                    {
                        activity.Values.Add(item.Key, item.Value);
                    }

                    activities.Add(activity);
                }
                return activities;
            }
        }

        public IEnumerable<IActivity> GetActivities(Func<IActivity, bool> predicate)
        {
            //using (ActivityDbContext db = new ActivityDbContext())
            //{
            //    var entityList = db.Activities.Where()

            //    List<IActivity> activities = new List<IActivity>();

            //    foreach (var entity in entityList)
            //    {
            //        activities.Add(new Activity
            //        {
            //            Action = entity.Action,
            //            Level = entity.Level,
            //            Recipient = entity.Recipient,
            //            Time = entity.Time,
            //            Values = entity.RelatedData != null ? JsonConvert.DeserializeObject<Dictionary<string, object>>(entity.RelatedData)
            //        });
            //    }
            //    return activities;
            //}

            throw new NotImplementedException();
        }
    }
}