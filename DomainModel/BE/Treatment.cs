﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.BE
{
    public class Treatment
    {

        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public TimeSpan Duration { get; set; }

        public TreatmentType TreatmentType { get; set; }

    }
}
