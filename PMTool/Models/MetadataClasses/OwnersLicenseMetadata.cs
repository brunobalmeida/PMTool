using Microsoft.AspNetCore.Mvc;
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
        public DateTime ExpireDate { get; set; }
        public string Active { get; set; }

        public ICollection<Person> Person { get; set; }
    }
}
