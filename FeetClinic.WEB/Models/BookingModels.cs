using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BE.BE;
using BE.BE.Schedule;

namespace FeetClinic.WEB.Models
{
    public class DayTimeSlotsViewModel
    {
        public DateTime Date { get; set; }
        public List<TimeSlot> TimeSlots { get; set; }
    }

    public class BookingViewModel
    {
        public Therapist Therapist { get; set; }
        public DayTimeSlotsViewModel DayTimeSlotsViewModel { get; set; } 
    }

}