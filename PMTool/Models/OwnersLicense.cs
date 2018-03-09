using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PMTool.Models
{
    public partial class OwnersLicense
    {
        public OwnersLicense()
        {
            Person = new HashSet<Person>();
        }

        public int OwnersLicenseId { get; set; }
        public string CompanyName { get; set; }
        public DateTime ExpireDate { get; set; }
        public string Active { get; set; }
        public string LicenseEmail { get; set; }

        public ICollection<Person> Person { get; set; }
    }
}
