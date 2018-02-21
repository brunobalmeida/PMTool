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



            yield return ValidationResult.Success;
        }

    }


    public class ProjectMetadata
    {
        public int ProjectId { get; set; }
        public int ClientId { get; set; }
        public int EmployeeId { get; set; }
        public int ProjectOpen { get; set; }
        public int ProjectName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string ProjectDescription { get; set; }

        public Client Client { get; set; }
        public Employee Employee { get; set; }
        public ICollection<TaskList> TaskList { get; set; }
    }
}
