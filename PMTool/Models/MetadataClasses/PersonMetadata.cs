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
            if (String.IsNullOrEmpty(FirstName))
            {
                yield return new ValidationResult("First Name can't be empty",
                       new[] { nameof(FirstName) });
            }
            else
            {
                FirstName = Validations.Capitalize(FirstName);
            }

            if (String.IsNullOrEmpty(LastName))
            {
                yield return new ValidationResult("Last Name can't be empty",
                       new[] { nameof(LastName) });
            }
            else
            {
                LastName = Validations.Capitalize(LastName);
            }
            
            MiddleName = Validations.Capitalize(MiddleName);


            if (Validations.EmailValidation(Email) == false)
            {
                yield return new ValidationResult("Email is not in a valid format",
                       new[] { nameof(Email) });
            }
            
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
        }
    }


    public class PersonMetadata
    {
        [Required]
        public int PersonId { get; set; }
        [Required]
        [Display(Name ="First Name")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Display(Name = "Middle Name")]
        public string MiddleName { get; set; }
        [Required]
        [Display(Name = "Address")]
        public string Address { get; set; }
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Required]
        public int OwnersLicenseId { get; set; }
        [Display(Name = "Address Line 2")]
        public string Address2 { get; set; }
        [Required]
        [Display(Name = "Province")]
        public int ProvinceId { get; set; }
        [Required]
        [Display(Name = "Postal Code")]
        public string PostalCode { get; set; }
        [Required]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
        [Display(Name = "Profile Picture")]
        public byte[] PersonImage { get; set; }
        public string ImageContentType { get; set; }
        public OwnersLicense OwnersLicense { get; set; }
        public Province Province { get; set; }
        public ICollection<Client> Client { get; set; }
        public ICollection<Employee> Employee { get; set; }

    }
}
