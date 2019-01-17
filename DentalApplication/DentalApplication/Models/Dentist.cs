using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SampleApplication.Models
{
    public class Dentist
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
        [StringLength(100, ErrorMessage = "Length of {0} must be less than {1}")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
    }
}