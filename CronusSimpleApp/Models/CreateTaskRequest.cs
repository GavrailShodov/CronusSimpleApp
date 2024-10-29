using System.ComponentModel.DataAnnotations;

namespace CronusSimpleApp.Models
{
    public class CreateTaskRequest
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public DateTimeOffset Deadline { get; set; }
    }
}
