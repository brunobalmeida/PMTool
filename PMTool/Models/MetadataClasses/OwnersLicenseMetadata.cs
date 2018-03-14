using Microsoft.AspNetCore.Mvc;
using PmToolClassLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PMTool.Models
{

    [ModelMetadataType(typeof(OwnersLicenseMetadata))]
    public partial class OwnersLicense : IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (ExpireDate > DateTime.Now)
            {
                Active = "Yes";
            }
            else
            {
                Active = "No"; 
            }

            if (String.IsNullOrEmpty(CompanyName))
            {
                yield return new ValidationResult("Company name cannot be null",
                      new[] { nameof(CompanyName) });
            }

            if (String.IsNullOrEmpty(LicenseEmail))
            {
                yield return new ValidationResult("Email cannot be null",
                      new[] { nameof(LicenseEmail) });
            }


            CompanyName = Validations.Capitalize(CompanyName);

            yield return ValidationResult.Success;
        }
    }

    public class OwnersLicenseMetadata
    {
        
        public int OwnersLicenseId { get; set; }
        [Required(ErrorMessage = "Company Name is Required")]
        [Display(Name = "Company Name")]
        public string CompanyName { get; set; }
        [Required]
        [Display(Name = "Expire Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd MMM yyyy}")]
        [DataType(DataType.Date)]
        public DateTime ExpireDate { get; set; }
        public string Active { get; set; }
        [EmailAddress]
        public string LicenseEmail { get; set; }

        public ICollection<Person> Person { get; set; }
    }
}
