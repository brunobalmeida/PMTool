using System;
using System.Collections.Generic;

namespace PMTool.Models
{
    public partial class ModelProject
    {
        public ModelProject()
        {
            ModelProjectTask = new HashSet<ModelProjectTask>();
        }

        public int ModelProjectId { get; set; }
        public string ModelName { get; set; }
        public string ModelDescription { get; set; }

        public ICollection<ModelProjectTask> ModelProjectTask { get; set; }
    }
}
