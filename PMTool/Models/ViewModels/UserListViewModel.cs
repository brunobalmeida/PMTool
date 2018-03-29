using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMTool.Models.ViewModels
{
    public class UserListViewModel
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsLocal { get; set; }
        public bool IsLocked { get; set; }
        public bool IsEmployee { get; set; }
        public bool IsOwner { get; set; }
        public bool IsClient { get; set; }

    }
}
