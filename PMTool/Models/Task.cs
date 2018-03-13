using System;
using System.Collections.Generic;

namespace PMTool.Models
{
    public partial class Task
    {
        public Task()
        {
            TaskInfo = new HashSet<TaskInfo>();
        }

        public int TaskId { get; set; }
        public int? EmployeeId { get; set; }
        public int TaskListId { get; set; }
        public string TaskName { get; set; }
        public int TaskWeight { get; set; }
        public string TaskDescription { get; set; }
        public DateTime ExpectedDate { get; set; }
        public int TaskDuration { get; set; }

        public Employee Employee { get; set; }
        public TaskList TaskList { get; set; }
        public ICollection<TaskInfo> TaskInfo { get; set; }
    }
}
