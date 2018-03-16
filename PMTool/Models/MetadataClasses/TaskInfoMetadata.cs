using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PMTool.Models
{
    [ModelMetadataType(typeof(TaskInfoMetadata))]
    public partial class TaskInfo : IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {

            if (String.IsNullOrEmpty(TaskNote))
            {
                yield return new ValidationResult("TaskNote Cannot Be empty",
                      new[] { nameof(TaskNote) });
            }
            else if (TaskNote.Length>255)
            {
                yield return new ValidationResult("TaskNote cannot have more than 255 characters.",
                      new[] { nameof(TaskNote) });
            }

            yield return ValidationResult.Success;
        }
    }


    public class TaskInfoMetadata
    {
        public int TaskInfoId { get; set; }

        [Display(Name ="Task Name")]
        public int TaskId { get; set; }

        [Display(Name = "Task Note")]
        public string TaskNote { get; set; }

        public Task Task { get; set; }
    }
}
