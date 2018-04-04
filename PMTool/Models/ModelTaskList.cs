using System;
using System.Collections.Generic;

namespace PMTool.Models
{
    public partial class ModelTaskList
    {
        public ModelTaskList()
        {
            ModelTask = new HashSet<ModelTask>();
        }

        public int ModelTaskListId { get; set; }
        public string ModelTaskListName { get; set; }
        public int ModelProjectId { get; set; }
        public int ModelTaskListOpen { get; set; }
        public ModelProject ModelProject { get; set; }
        public ICollection<ModelTask> ModelTask { get; set; }
    }
}
