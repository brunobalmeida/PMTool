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


            //Check if ProjectName string is null or empty
            if (String.IsNullOrEmpty(ProjectName))
            {
                yield return new ValidationResult("You should insert the project name", new string[] { "ProjectName" });

            }
            else
            {
                yield return ValidationResult.Success;
            }

            //Capitalise Project Name
            string aux = ProjectName;
            ProjectName = Validations.Capitalize(aux);

            //validate if the description is null or empty
            if (String.IsNullOrEmpty(ProjectDescription))
            {
                yield return new ValidationResult("You should insert a project description",
                    new[] {nameof(ProjectDescription) });
            }
            else
            {
                yield return ValidationResult.Success;
            }

            //validation Project Open
            if (ProjectOpen == 1 || ProjectOpen == 0)
            {
                yield return ValidationResult.Success;
            }
            else
            {
                yield return new ValidationResult("Project status can not be different than 1 or 0", new string[] { "ProjectOpen" });
            }


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
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }
        [Display(Name = "End Date")]
        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }
        [Display(Name = "Project Description")]
        public string ProjectDescription { get; set; }
        public Client Client { get; set; }
        public Employee Employee { get; set; }
        public ICollection<TaskList> TaskList { get; set; }
    }
}
