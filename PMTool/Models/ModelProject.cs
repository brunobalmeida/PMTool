using System;
using System.Collections.Generic;

namespace PMTool.Models
{
    public partial class ModelProject
    {
        public ModelProject()
        {
            ModelTaskList = new HashSet<ModelTaskList>();
        }

        public int ModelProjectId { get; set; }
        public int ModelProjectOpen { get; set; }
        public string ModelProjectName { get; set; }
        public string ModelProjectDescription { get; set; }

        public ICollection<ModelTaskList> ModelTaskList { get; set; }
    }
}
