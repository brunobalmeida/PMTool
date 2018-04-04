using System;
using System.Collections.Generic;

namespace PMTool.Models
{
    public partial class ModelTask
    {
        public ModelTask()
        {
            ModelTaskInfo = new HashSet<ModelTaskInfo>();
        }

        public int ModelTaskId { get; set; }
        public int ModelTaskListId { get; set; }
        public string ModelTaskName { get; set; }
        public int ModelTaskWeight { get; set; }
        public string ModelTaskDescription { get; set; }
        public DateTime ModelExpectedDate { get; set; }
        public int? ModelTaskDuration { get; set; }
        public int? ModelTaskActiveFlag { get; set; }

        public ModelTaskList ModelTaskList { get; set; }
        public ICollection<ModelTaskInfo> ModelTaskInfo { get; set; }
    }
}
