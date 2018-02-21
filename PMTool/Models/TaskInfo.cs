using System;
using System.Collections.Generic;

namespace PMTool.Models
{
    public partial class TaskInfo
    {
        public int TaskInfoId { get; set; }
        public int TaskId { get; set; }
        public string TaskNote { get; set; }

        public Task Task { get; set; }
    }
}
