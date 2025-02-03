using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NatlovAssighment.Models
{
    public class CourseSchedule
    {
        public int Id { get; set; }

        [Required]
        public int CourseId { get; set; }
        [ForeignKey("CourseId")]
        public Course Course { get; set; }

        [Required]
        public int TeacherId { get; set; }
        [ForeignKey("TeacherId")]
        public Teacher Teacher { get; set; }

        [Required]
       // [Range(typeof(TimeSpan), "00:00:00", "23:59:59.9999999", ErrorMessage = "Start Time must be between 00:00:00 and 23:59:59.9999999.")]
        public TimeSpan StartTime { get; set; }

        [Required]
        public TimeSpan EndTime { get; set; }
    }
}
