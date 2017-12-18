using System;
using System.Collections.Generic;

namespace ActivityTracker
{
    public interface IActivity
    {
        string Recipient { get; set; }

        string Action { get; set; }

        DateTime Time { get; set; }

        ActivityLevel Level { get; set; }

        Dictionary<string, object> Values { get; set; }
    }
}