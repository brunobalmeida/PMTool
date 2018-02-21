using System;
using System.Collections.Generic;

namespace PMTool.Models
{
    public partial class Role
    {
        public Role()
        {
            Person = new HashSet<Person>();
        }

        public int RoleId { get; set; }
        public string RolePrivilegies { get; set; }

        public ICollection<Person> Person { get; set; }
    }
}
