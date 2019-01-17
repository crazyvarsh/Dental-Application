using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SampleApplication.Models
{
    public class Patient
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "Length of {0} must be less than {1}")]
        public string FirstName { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "Length of {0} must be less than {1}")]
        public string LastName { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "Length of {0} must be less than {1}")]
        public string EmergencyContactName { get; set; }
        [Required]
        [RegularExpression(@"^[2-9][0-9]{2}?[-. ]?([0-9]{3})?[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
        public string EmergencyContactPhone { get; set; }
        public int DentistID { get; set; }
        public Dentist Dentist { get; set; }
        public Patient()
        {
            this.DentistID = 2;
        }
    }
}