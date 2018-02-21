using System;
using System.Collections.Generic;

namespace PMTool.Models
{
    public partial class ModelProjectTask
    {
        public int ModelProjectTaskId { get; set; }
        public int ModelTaskId { get; set; }
        public int ModelProjectId { get; set; }

        public ModelProject ModelProject { get; set; }
        public ModelTask ModelTask { get; set; }
    }
}
