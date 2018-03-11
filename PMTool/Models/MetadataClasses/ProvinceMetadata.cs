using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using PmToolClassLibrary;

namespace PMTool.Models
{
    [ModelMetadataType(typeof(ProvinceMetadata))]
    public partial class Province : IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {

            if (String.IsNullOrEmpty(ProvinceName))
            {
                yield return new ValidationResult("Province Name cannot be null",
                     new[] { nameof(ProvinceName) });
            }
            else
            {
                ProvinceName = Validations.Capitalize(ProvinceName);
            }

            if (String.IsNullOrEmpty(ProvinceCode))
            {
                yield return new ValidationResult("Province Code cannot be null.",
                     new[] { nameof(ProvinceCode) });
            }
            
            ProvinceCode = ProvinceCode.Trim().ToUpper();

            if (ProvinceCode.Length > 2)
            {
                yield return new ValidationResult("Province Code cannot be greater than 2 characters",
                     new[] { nameof(ProvinceCode) });
            }
            

            yield return ValidationResult.Success;
        }
    }

    public class ProvinceMetadata
    {
        public int ProvinceId { get; set; }
        [Display(Name = "Province Name")]
        public string ProvinceName { get; set; }
        [Display(Name = "Province Code")]
        public string ProvinceCode { get; set; }
        public int CountryId { get; set; }
        public Country Country { get; set; }
        public ICollection<Person> Person { get; set; }
    }
}
