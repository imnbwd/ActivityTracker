using System;
using System.Collections.Generic;

namespace ActivityTracker
{
    public class Activity : IActivity
    {
        public Activity()
        {
            Values = new Dictionary<string, object>();
        }

        public string Action { get; set; }
        public ActivityLevel Level { get; set; }
        public string Recipient { get; set; }

        public DateTime Time { get; set; }

        /// <summary>
        /// A dictionary contained some addtional info
        /// </summary>
        public Dictionary<string, object> Values { get; }
    }
}