using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BE.BE;
using BE.BE.Schedule;
using BE.BE.Treatments;
using BE.Interfaces;

namespace FeetClinic.WEB.Models
{
    public class DayTimeSlotViewModel
    {
        public DateTime Date { get; set; }
        public List<ITimeSlot> TimeSlots { get; set; }
    }

    public class CreateBookingViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Choose therapist")]
        public IEnumerable<SelectListItem> TherapistsSelectListItems { get; set; }
        [Required]
        public int SelectedTherapistId { get; set;}
        [Display(Name = "Choose treatments")]
        public IEnumerable<SelectListItem> TreatmentsSelectListItems { get; set; } 
        [Required]
        public int[] SelectedTreatmentsId { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        [Display(Name = "Choose date")]
        public DateTime DateTime { get; set; }
        public DateTime BookingDateTime { get; set; }

        public List<DayTimeSlotViewModel> WeekFreeTimes { get; set; } 

    }

}