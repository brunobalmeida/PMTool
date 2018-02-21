using System;
using System.Collections.Generic;

namespace PMTool.Models
{
    public partial class Province
    {
        public Province()
        {
            Person = new HashSet<Person>();
        }

        public int ProvinceId { get; set; }
        public string ProvinceName { get; set; }
        public string ProvinceCode { get; set; }
        public int CountryId { get; set; }

        public Country Country { get; set; }
        public ICollection<Person> Person { get; set; }
    }
}
