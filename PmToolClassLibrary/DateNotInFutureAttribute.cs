using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PmToolClassLibrary
{
    public class DateNotInFutureAttribute : ValidationAttribute
    {
        public DateNotInFutureAttribute()
        {
            ErrorMessage = "{0} can not be in the future";
        }


        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success;
            }
            DateTime aux = (DateTime)value;

            if (aux > DateTime.Now)
            {
                return new ValidationResult(
                    string.Format(ErrorMessage, validationContext.DisplayName));
            }
            else
            {
                return ValidationResult.Success;
            }

        }
    }
}
