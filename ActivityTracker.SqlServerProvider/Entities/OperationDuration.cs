using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ActivityTracker.SqlServerProvider.Entities
{
    public class OperationDuration
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Action { get; set; }

        public TimeSpan Duration { get; set; }

        public ActivityLevel Level { get; set; }
    }
}