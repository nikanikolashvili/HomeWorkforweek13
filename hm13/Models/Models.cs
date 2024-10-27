using System.ComponentModel.DataAnnotations;

namespace hm13.Models
{
    public class Appointment
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Doctor { get; set; }

        [Required]
        [RegularExpression(@"^(1[0-9]|0?[0-9]):[0-5][0-9]$", ErrorMessage = "Invalid time format. Use HH:mm.")]
        public string Time { get; set; }
    }
}
