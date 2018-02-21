using System;
using System.Collections.Generic;

namespace PMTool.Models
{
    public partial class Country
    {
        public Country()
        {
            Province = new HashSet<Province>();
        }

        public int CountryId { get; set; }
        public string CountryName { get; set; }

        public ICollection<Province> Province { get; set; }
    }
}
