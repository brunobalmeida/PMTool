using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMTool.Models
{
    public partial class ModelTaskList
    {
        public ModelTaskList()
        {
            ModelProject = new HashSet<ModelProject>();
        }

        public int ModelTaskListId { get; set; }
        public string ModelTaskListName { get; set; }
        public int ModelProjectId { get; set; }
        public int ModelTaskListOpen { get; set; }

        public ICollection<ModelProject> ModelProject { get; set; }
    }
}
