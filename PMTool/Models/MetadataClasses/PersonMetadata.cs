using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PMTool.Models
{
    [ModelMetadataType(typeof(PersonMetadata))]
    public partial class Person : IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            



            yield return ValidationResult.Success;
        }
    }


    public class PersonMetadata
    {
        public int PersonId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        [Required]
        public string Address { get; set; }
        public string Email { get; set; }
        public int OwnersLicenseId { get; set; }
        public string Address2 { get; set; }
        public int ProvinceId { get; set; }
        public string PostalCode { get; set; }
        public string PhoneNumber { get; set; }
        public byte[] PersonImage { get; set; }

        public OwnersLicense OwnersLicense { get; set; }
        public Province Province { get; set; }
        public ICollection<Client> Client { get; set; }
        public ICollection<Employee> Employee { get; set; }

    }
}
