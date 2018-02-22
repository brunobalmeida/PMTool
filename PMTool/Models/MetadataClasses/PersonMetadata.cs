using Microsoft.AspNetCore.Mvc;
using PmToolClassLibrary;
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
            FirstName = Validations.Capitalize(FirstName);
            LastName = Validations.Capitalize(LastName);
            MiddleName = Validations.Capitalize(MiddleName);

            string tempPostalCode = PostalCode;
            if (Validations.PostalCodeValidation(ref tempPostalCode))
            {
                PostalCode = tempPostalCode;
            }
            else
            {
                yield return new ValidationResult("Postal Code is not in a valid format",
                       new[] { nameof(PostalCode) });
            }

            string tempPhoneNumber = PhoneNumber;
            if (Validations.PhoneValidation(ref tempPhoneNumber))
            {
                PhoneNumber = tempPhoneNumber;
            }
            else
            {
                yield return new ValidationResult("Phone Number is not in a valid format",
                       new[] { nameof(PhoneNumber) });
            }


            yield return ValidationResult.Success;



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
