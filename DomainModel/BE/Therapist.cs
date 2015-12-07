using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE.BE.Schedule;
using BE.BE.Treatments;
using BE.Interfaces;

namespace DomainModel.BE
{
    public class Therapist : IEntity
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        
        public virtual  List<Treatment> Treatments { get; set; }
        public virtual List<DayWorkingHours> WorkingHourses { get; set; }
        public virtual List<YearsScheduler> Schedulers { get; set; } 

    }
}
