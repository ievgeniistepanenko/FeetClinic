using System;
using System.Collections.Generic;
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
        public IEnumerable<SelectListItem> Therapists { get; set; }
        public IEnumerable<SelectListItem> Treatments { get; set; } 
        public List<DayTimeSlotViewModel> DayTimeSlotsViewModel { get; set; } 
    }

}