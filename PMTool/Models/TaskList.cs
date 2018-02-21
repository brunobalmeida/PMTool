using System;
using System.Collections.Generic;

namespace PMTool.Models
{
    public partial class TaskList
    {
        public TaskList()
        {
            Task = new HashSet<Task>();
        }

        public int TaskListId { get; set; }
        public string TaskListName { get; set; }
        public int ProjectId { get; set; }
        public int TaskListOpen { get; set; }

        public Project Project { get; set; }
        public ICollection<Task> Task { get; set; }
    }
}
