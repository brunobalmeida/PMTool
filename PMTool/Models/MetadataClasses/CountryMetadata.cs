using Microsoft.AspNetCore.Mvc;
using PmToolClassLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PMTool.Models
{
    [ModelMetadataType(typeof(CountryMetadata))]
    public partial class Country : IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (String.IsNullOrEmpty(CountryName))
            {
                yield return new ValidationResult("Country Name cannot be null",
                      new[] { nameof(CountryName) });
            }
            else
            {
                CountryName = Validations.Capitalize(CountryName);
            }


            yield return ValidationResult.Success;
        }
    }
    

    public class CountryMetadata
    {
        public int CountryId { get; set; }
        [Display(Name = "Country Name")]
        public string CountryName { get; set; }

        public ICollection<Province> Province { get; set; }
    }
}
