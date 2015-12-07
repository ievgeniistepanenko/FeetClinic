using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FeetClinic.WEB.Models
{
    public class Treatment
    {
        public int Id { get; }

        public TreatmentType Type { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int Price { get; set; }

        public int Duration { get; set; }


    }

    public class TreatmentType
    {
        public int Id { get; }
        public string Name { get; set; }

    }
}