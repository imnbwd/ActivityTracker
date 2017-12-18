using System;

namespace ActivityTracker
{
    public class OperationDuration : IOperationDuration
    {
        public string Action { get; set; }
        public TimeSpan Duration { get; set; }
        public ActivityLevel Level { get; set; }
    }
}