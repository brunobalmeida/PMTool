using PmToolClassLibrary;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PMTool.Models
{
    [ModelMetadataType(typeof(ProjectMetadata))]
    public partial class Project : IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            //validation of end date
            if (EndDate > StartDate)
            {
                yield return ValidationResult.Success;
            }
            else
            {
                yield return new ValidationResult("End date can not be earlier than start date", new string[] { "EndDate" });
            }

            //Capitalize Project Name
            string aux = ProjectName;
            ProjectName = Validations.Capitalize(aux);

        }
    }


    public class ProjectMetadata
    {

        public int ProjectId { get; set; }
        public int ClientId { get; set; }
        public int EmployeeId { get; set; }
        [Display(Name = "Open Project")]
        public int ProjectOpen { get; set; }
        [Display(Name = "Project Name")]
        public string ProjectName { get; set; }
        [Display(Name = "Start Date")]
        [DateNotInFuture]
        public DateTime StartDate { get; set; }
        [Display(Name = "End Date")]
        public DateTime? EndDate { get; set; }
        [Display(Name = "Project Description")]
        public string ProjectDescription { get; set; }
        public Client Client { get; set; }
        public Employee Employee { get; set; }
        public ICollection<TaskList> TaskList { get; set; }
    }
}
