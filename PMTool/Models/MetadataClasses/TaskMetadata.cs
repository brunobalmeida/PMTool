using Microsoft.AspNetCore.Mvc;
using PmToolClassLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PMTool.Models
{
    [ModelMetadataType(typeof(TaskMetadata))]
    public partial class Task : IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {

            if (String.IsNullOrEmpty(TaskName))
            {
                yield return new ValidationResult("Task name cannot be empty",
                       new[] { nameof(TaskName) });
            }
            else
            {
                TaskName = Validations.Capitalize(TaskName.Trim());
            }

            if (String.IsNullOrEmpty(TaskDescription))
            {
                yield return new ValidationResult("Task name cannot be empty",
                       new[] { nameof(TaskDescription) });
            }


            if (TaskDuration < 0 )
            {
                yield return new ValidationResult("Task duration cannot be less than 0 days",
                       new[] { nameof(TaskDuration) });
            }

            if (ExpectedDate < DateTime.Now)
            {
                yield return new ValidationResult("The due date must be in the future.",
                       new[] { nameof(ExpectedDate) });
            }



            yield return ValidationResult.Success;
        }
    }





    public class TaskMetadata
    {
        public int TaskId { get; set; }
        
        [Display(Name ="Colaborator")]
        public int? EmployeeId { get; set; }
        [Required]
        [Display(Name = "Task List")]
        public int TaskListId { get; set; }

        [Display(Name = "Task")]
        public string TaskName { get; set; }

        [Display(Name = "Weight")]
        public int TaskWeight { get; set; }

        [Display(Name = "Description")]
        public string TaskDescription { get; set; }

        [Display(Name = "Due To:")]
        [DataType(DataType.Date)]
        public DateTime ExpectedDate { get; set; }

        [Display(Name = "Duration")]
        public int TaskDuration { get; set; }

        [Display(Name = "Status?")]
        public int TaskActiveFlag { get; set; }

        public Employee Employee { get; set; }
        public TaskList TaskList { get; set; }
        public ICollection<TaskInfo> TaskInfo { get; set; }


    }
}
