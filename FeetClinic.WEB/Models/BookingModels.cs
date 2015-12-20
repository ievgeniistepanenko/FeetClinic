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

        [Display(Name = "Choose therapist")]
        public IEnumerable<SelectListItem> TherapistsSelectListItems { get; set; }

        [Required(ErrorMessage = "Choose some therapist")]
        public int therapistId { get; set;}

        [Display(Name = "Choose treatments")]
        public IEnumerable<SelectListItem> TreatmentsSelectListItems { get; set; }

        [Required(ErrorMessage = "Choose some treatments")]
        public int[] SelectedTreatmentsId { get; set; }

        [Required(ErrorMessage = "Choose some date")]
        [Display(Name = "Choose date")]
        public DateTime date { get; set; }

        [Required(ErrorMessage = "Choose time")]
        [Display(Name = "Choose time")]
        public DateTime Time { get; set; }


        public List<DayTimeSlotViewModel> WeekFreeTimes { get; set; } 

    }

}