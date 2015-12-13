using BE.BE;
using BE.BE.Schedule;
using BE.BE.Treatments;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FeetClinic.WEB.Models
{
    public class TherapistViewModel
    {

        [Display(Name = "Treatments")]
        public int SelectedTreatmentId { get; set; }
        public IEnumerable<SelectListItem> treats { get; set; }



        public int Id { get; }

        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }

        public List<Holiday> Holidays { get; set; }
        public List<Treatment> Treatments { get; set; }
        public List<DayWorkingHours> WorkingHourses { get; set; }
    }

    public class TreatmentViewModel
    {
        [Display(Name = "Treatment Type")]
        public int SelectedTypeId { get; set; }
        public IEnumerable<SelectListItem> types { get; set; }

        [Display(Name = "Therapist")]
        public int SelectedTherapistId { get; set; }
        public IEnumerable<SelectListItem> theras { get; set; }

        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }
        [Required]
        public TimeSpan Duration { get; set; }
        [Required]
        public virtual TreatmentType TreatmentType { get; set; }
        [Required]
        public List<Therapist> Therapists { get; set; }
        public List<Booking> Bookings { get; set; }

    }

}