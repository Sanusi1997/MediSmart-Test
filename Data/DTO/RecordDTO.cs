using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediSmart.API.Data.DTO
{
    public class RecordDTO
    {
        public int PatientId { get; set; }
        public string Name { get; set; }
        public string Occupation { get; set; }
        public string HMOProvider { get; set; }
        public int Age { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string DateOfVisit { get; set; }
        public string NextOfKinName { get; set; }
        public string Illness { get; set; }
        public string HomeAddress { get; set; }

    }
}
