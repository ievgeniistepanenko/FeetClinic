using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel.BE.Schedule;

namespace DomainModel.BE
{
    public class Therapist
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public byte [] Image { get; set; }

        public List<Treatment> Treatments { get; set; } 
        public List<YearsScheduler> Schedulers { get; set; } 

    }
}
