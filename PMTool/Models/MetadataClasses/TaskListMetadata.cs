using Microsoft.AspNetCore.Mvc;
using PmToolClassLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PMTool.Models
{
    [ModelMetadataType(typeof(TaskListMetadata))]
    public partial class TaskList : IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (String.IsNullOrEmpty(TaskListName))
            {
                yield return new ValidationResult("Task List Name cannot be empty.",
                       new[] { nameof(TaskListName) });
            }
            else
            {
                TaskListName = Validations.Capitalize(TaskListName);
            }
            
            yield return ValidationResult.Success;
        }
    }


    public class TaskListMetadata
    {
        public int TaskListId { get; set; }

        [Display(Name ="Task List Name")]
        public string TaskListName { get; set; }

        [Display(Name = "Project Name")]
        public int ProjectId { get; set; }

        [Display(Name = "Task List Status")]
        public int TaskListOpen { get; set; }

        public Project Project { get; set; }
        public ICollection<Task> Task { get; set; }
    }
}
