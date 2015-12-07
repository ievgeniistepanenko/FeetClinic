using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel.BE.Schedule;
using DomainModel.BE.Treatments;
using DomainModel.Interfaces;

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
        public virtual List<Holiday> Holidays { get; set; }
        public virtual List<DayWorkingHours> WorkingHourses { get; set; }

    }
}
