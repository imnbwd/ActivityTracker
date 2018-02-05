using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ActivityTracker.SqlServerProvider
{
    /// <summary>
    /// Represents an activity of the user
    /// </summary>
    public class Activity
    {
        [Required]
        public string Action { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }

        /// <summary>
        /// The level
        /// </summary>
        public ActivityLevel Level { get; set; }

        /// <summary>
        /// The recipient
        /// </summary>
        public string Recipient { get; set; }

        /// <summary>
        /// A json string that represents a dictionary, that dictionary may store some additional info
        /// </summary>
        public string RelatedData { get; set; }

        /// <summary>
        /// The time of the activity occured
        /// </summary>
        public DateTime Time { get; set; }

    }
}