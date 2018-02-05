using System;

namespace ActivityTracker
{
    /// <summary>
    /// The activity level
    /// </summary>
    [Flags]
    public enum ActivityLevel
    {
        Unknown = 0,

        /// <summary>
        /// The activity on app level (e.g. app start and shutdown)
        /// </summary>
        Application = 1,

        /// <summary>
        /// The activity on view level (e.g. window opened and closed)
        /// </summary>
        View = 2,

        /// <summary>
        /// The activity on an action level (e.g. click a button, perform a database query)
        /// </summary>
        Action = 3
    }
}