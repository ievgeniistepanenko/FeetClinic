using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        
<<<<<<< HEAD
        public virtual  List<Treatment> Treatments { get; set; } 

=======
        public virtual  List<Treatment> Treatments { get; set; }
        public virtual List<DayWorkingHours> WorkingHourses { get; set; }
>>>>>>> c3d4cfab2133fec978445d4bffeb97cf0e0bb162
        public virtual List<YearsScheduler> Schedulers { get; set; } 

    }
}
