using System;
using System.Collections.Generic;

namespace PMTool.Models
{
    public partial class ModelTask
    {
        public ModelTask()
        {
            ModelProjectTask = new HashSet<ModelProjectTask>();
        }

        public int ModelTaskId { get; set; }
        public int ModelTaskListId { get; set; }
        public string ModelTaskName { get; set; }
        public string ModelTaskWeight { get; set; }
        public string ModelTaskDescription { get; set; }
        public int ModelExpectedDate { get; set; }
        public int ModelTaskDuration { get; set; }
        public int ModelTaskActiveFlag { get; set; }

        public ICollection<ModelProjectTask> ModelProjectTask { get; set; }
    }
}
