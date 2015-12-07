using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FeetClinic.WEB.Models
{
    public class CustomerModels
    {
        public int Id { get; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string StreetName { get; set; }

        public string StreetNumber { get; set; }

        public string City { get; set; }

        public int ZipCode { get; set; }

    }
}