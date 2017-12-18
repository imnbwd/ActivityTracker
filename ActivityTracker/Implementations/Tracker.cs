using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace ActivityTracker
{
    public class Tracker : ITracker
    {
        private readonly IActivityRepository _activityRepository;
        private readonly IOperationDurationRepository _operationDurationRepository;

        private ConcurrentDictionary<string, IActivity> _tokenDictionary;

        public Tracker(IActivityRepository activityRepository, IOperationDurationRepository operationDurationRepository)
        {
            _activityRepository = activityRepository;
            _operationDurationRepository = operationDurationRepository;
        }

        public string AppName { get; private set; }

        #region TrackForApp

        public void EndTrack()
        {
            var exitActivity = new Activity
            {
                Recipient = AppName,
                Action = Constant.ACTION_Exit,
                Level = ActivityLevel.Application,
                Time = DateTime.Now,
                Values = null
            };

            _activityRepository.Add(exitActivity);

            var startActivity = _activityRepository
                .GetActivities()
                .Where(a => a.Recipient == AppName && a.Action == Constant.ACTION_Startup)
                .OrderByDescending(a => a.Time)
                .FirstOrDefault();

            if (startActivity != null)
            {
                var duration = exitActivity.Time - startActivity.Time;
                _operationDurationRepository.Add(new OperationDuration
                {
                    Level = ActivityLevel.Application,
                    Duration = duration,
                    Action = Constant.ACTION_RuningApp
                });
            }

            _tokenDictionary.Clear();
        }

        public void StartTrack(string appName)
        {
            AppName = appName;

            _tokenDictionary = new ConcurrentDictionary<string, IActivity>();
            _activityRepository.Add(new Activity
            {
                Recipient = AppName,
                Action = Constant.ACTION_Startup,
                Level = ActivityLevel.Application,
                Time = DateTime.Now,
                Values = null
            });
        }

        #endregion TrackForApp

        #region TrackForAction

        public void TrackActivity(IActivity activity)
        {
            if (activity == null)
            {
                throw new ArgumentNullException(nameof(activity));
            }

            if (string.IsNullOrWhiteSpace(activity.Action))
            {
                throw new ArgumentNullException(nameof(activity.Action));
            }

            _activityRepository.Add(new Activity
            {
                Recipient = activity.Recipient,
                Level = ActivityLevel.Action,
                Time = DateTime.Now,
                Action = activity.Action,
                Values = activity.Values
            });
        }

        public void TrackActivity(string action)
        {
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            TrackActivity(new Activity { Action = action });
        }

        public void TrackActivity(string action, Dictionary<string, object> values)
        {
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            TrackActivity(new Activity { Action = action, Values = values });
        }

        public void TrackActivityEnd(string token)
        {
            IActivity activity = null;
            _tokenDictionary.TryGetValue(token, out activity);

            if (activity == null)
            {
                return;
            }

            var startTime = activity.Time;
            var endTime = DateTime.Now;

            activity.Time = endTime;
            _activityRepository.Add(activity);

            _operationDurationRepository.Add(new OperationDuration
            {
                Action = activity.Action,
                Duration = endTime - startTime,
                Level = ActivityLevel.Action
            });
        }

        public string TrackActivityStart(IActivity activity)
        {
            TrackActivity(activity);

            var token = GenerateToken(activity.Action, activity.Time);
            _tokenDictionary.TryAdd(token, activity);

            return token;
        }

        public string TrackActivityStart(string action)
        {
            return TrackActivityStart(new Activity
            {
                Action = action,
                Level = ActivityLevel.Action,
                Time = DateTime.Now,
            });
        }

        private string GenerateToken(string action, DateTime actionTime)
        {
            return action + actionTime.Ticks.ToString();
        }

        #endregion TrackForAction

        #region TrackForView

        public void TrackViewEnd(string viewName)
        {
            if (viewName == null)
            {
                throw new ArgumentNullException(nameof(viewName));
            }

            var endActivity = new Activity
            {
                Recipient = viewName,
                Action = Constant.ACTION_ViewUnloaded,
                Level = ActivityLevel.View,
                Time = DateTime.Now,
                Values = null
            };

            _activityRepository.Add(endActivity);

            var startActivity = _activityRepository
                    .GetActivities()
                    .Where(a => a.Recipient == viewName && a.Action == Constant.ACTION_ViewLoaded)
                    .OrderByDescending(a => a.Time)
                    .FirstOrDefault();

            if (startActivity != null)
            {
                var duration = endActivity.Time - startActivity.Time;
                _operationDurationRepository.Add(new OperationDuration
                {
                    Duration = duration,
                    Action = Constant.ACTION_ShowingView,
                    Level = ActivityLevel.View
                });
            }
        }

        public void TrackViewStart(string viewName)
        {
            if (viewName == null)
            {
                throw new ArgumentNullException(nameof(viewName));
            }

            _activityRepository.Add(new Activity
            {
                Recipient = viewName,
                Action = Constant.ACTION_ViewLoaded,
                Level = ActivityLevel.View,
                Time = DateTime.Now,
                Values = null
            });
        }

        #endregion TrackForView
    }
}