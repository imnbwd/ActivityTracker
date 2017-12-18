using System;

namespace ActivityTracker
{
    public interface IOperationDuration
    {
        TimeSpan Duration { get; set; }

        string Action { get; set; }

        ActivityLevel Level { get; set; }
    }
}