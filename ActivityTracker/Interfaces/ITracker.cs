using System.Collections.Generic;

namespace ActivityTracker
{
    public interface ITracker
    {
        void EndTrack();

        void StartTrack(string appName);

        void TrackActivity(IActivity activity);

        void TrackActivity(string action, Dictionary<string, object> values);

        void TrackActivity(string action);

        void TrackActivityEnd(string token);

        string TrackActivityStart(IActivity activity);

        string TrackActivityStart(string action);

        void TrackViewEnd(string viewName);

        void TrackViewStart(string viewName);
    }
}