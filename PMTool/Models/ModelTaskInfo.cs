using System;
using System.Collections.Generic;

namespace PMTool.Models
{
    public partial class ModelTaskInfo
    {
        public int ModelTaskInfoId { get; set; }
        public int ModelTaskId { get; set; }
        public string ModelTaskNote { get; set; }

        public ModelTask ModelTask { get; set; }
    }
}
