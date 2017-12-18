using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ActivityTracker.SqlServerProvider.Entities
{
    public class Activity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Action { get; set; }

        public ActivityLevel Level { get; set; }
        public string Recipient { get; set; }

        public DateTime Time { get; set; }

        public string RelatedData { get; set; }
    }
}