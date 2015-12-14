using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BE.BE;
using BE.BE.Schedule;
using BE.BE.Treatments;

namespace FeetClinic.WEB.Models
{
    public class DayTimeSlotsViewModel
    {
        public DateTime Date { get; set; }
        public List<TimeSlot> TimeSlots { get; set; }
    }

    public class CreateBookingViewModel
    {
        public List<Therapist> Therapists { get; set; }
        public List<Treatment> Treatments { get; set; } 
        public DayTimeSlotsViewModel DayTimeSlotsViewModel { get; set; } 
    }

}