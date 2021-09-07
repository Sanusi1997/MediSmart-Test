using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MediSmart.API.Entities
{
    public class Record
    {
        [Key]

        [Required]
        public int PatientId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Occupation { get; set; }
        [Required]
        public string HMOProvider { get; set; }
        [Required]
        public int Age { get; set; }
        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; }
        [Required, MaxLength(14)]
        public string PhoneNumber { get; set; }
        [Required]
        public string DateOfVisit { get; set; }
        [Required]
        public string NextOfKinName { get; set; }
        [Required]
        public string Illness { get; set; }
        [Required]
        public string HomeAddress { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }


    }
}
