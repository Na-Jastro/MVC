using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UploadAndReadingExcelFile.Models
{
    public class Mentor
    {
        public string Id { get; set; }
        public string Firstname { get; set; }

        public string Lastname { get; set; }
        public string OrganisationId { get; set; }
        public string EmailAddress { get; set; }

        public string ProfessionalNumber { get; set; }

        public string Position { get; set; }

        public string DateHired { get; set; }
    }
}
