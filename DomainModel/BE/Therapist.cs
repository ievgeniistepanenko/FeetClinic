using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BE.BE.Schedule;
using BE.BE.Treatments;
using BE.Interfaces;

namespace BE.BE
{
    public class Therapist : IEntity
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        public virtual List<Holiday> Holidays { get; set; }
        public virtual List<DayWorkingHours> WorkingHourses { get; set; }
        
        public virtual  List<Treatment> Treatments { get; set; } 

    }
}
