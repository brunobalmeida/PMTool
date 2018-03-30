using System;
using System.Collections.Generic;

namespace PMTool.Models
{
    public partial class Person
    {
        public Person()
        {
            Client = new HashSet<Client>();
            Employee = new HashSet<Employee>();
        }

        public int PersonId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public int OwnersLicenseId { get; set; }
        public string Address2 { get; set; }
        public int ProvinceId { get; set; }
        public string PostalCode { get; set; }
        public string PhoneNumber { get; set; }
        public byte[] PersonImage { get; set; }
        public string ImageContentType { get; set; }
        public OwnersLicense OwnersLicense { get; set; }
        public Province Province { get; set; }
        public ICollection<Client> Client { get; set; }
        public ICollection<Employee> Employee { get; set; }

        public string GetPicture()
        {
            if (PersonImage == null)
            {
                return null;
            }

            var base64Image = System.Convert.ToBase64String(PersonImage);
            return $"data:{ImageContentType};base64,{base64Image}";
        }


    }
}
