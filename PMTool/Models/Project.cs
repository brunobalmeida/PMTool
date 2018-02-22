using System;
using System.Collections.Generic;

namespace PMTool.Models
{
    public partial class Project
    {
        public Project()
        {
            TaskList = new HashSet<TaskList>();
        }

        public int ProjectId { get; set; }
        public int ClientId { get; set; }
        public int EmployeeId { get; set; }
        public int ProjectOpen { get; set; }
        public string ProjectName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string ProjectDescription { get; set; }

        public Client Client { get; set; }
        public Employee Employee { get; set; }
        public ICollection<TaskList> TaskList { get; set; }
    }
}
