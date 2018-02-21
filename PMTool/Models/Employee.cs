using System;
using System.Collections.Generic;

namespace PMTool.Models
{
    public partial class Employee
    {
        public Employee()
        {
            Project = new HashSet<Project>();
            Task = new HashSet<Task>();
        }

        public int EmployeeId { get; set; }
        public int PersonId { get; set; }
        public int? EmployeeNumber { get; set; }

        public Person Person { get; set; }
        public ICollection<Project> Project { get; set; }
        public ICollection<Task> Task { get; set; }
    }
}
