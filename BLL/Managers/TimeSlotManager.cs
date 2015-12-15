using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE.BE;
using BE.BE.Schedule;
using BE.Interfaces;

namespace BLL.Managers
{
    public class TimeSlotManager
    {
        private DayWorkingHoursManager _whManager;
        private DayAgendasManager _dayAgendasManager;
        private HolidayManager _holidayManager;
        private BookingsManager _bookingsManager;

        private void InstanceMembers()
        {
            _whManager = new DayWorkingHoursManager();
            _dayAgendasManager = new DayAgendasManager();
            _holidayManager = new HolidayManager();
            _bookingsManager = new BookingsManager();
        }

        public List<List<ITimeSlot>> GetAvailableTimeSlots(int therapistId, int week, int year)
        {
            InstanceMembers();

            List<Holiday> holidaysForTherapist = _holidayManager.GetAllForTherapist(therapistId);
            DateTime firstDate = FirstDateOfWeekISO8601(year, week);

            List<DayWorkingHours> dayWorkingHourses = _whManager.GetAllWorkingHours(therapistId).ToList();

            List<Booking> bookings = _bookingsManager.
                GetAllForTherapistForWeek(therapistId, week, year, "Treatments").ToList();

            List<List<ITimeSlot>> timeSlots = new List<List<ITimeSlot>>();

            for (int i = 0; i < 7; i++)
            {
                DateTime tempDate = firstDate.AddDays(i);
                List<ITimeSlot> ts = new List<ITimeSlot>();
                if (holidaysForTherapist.Any(holiday => holiday.IsHoliday(tempDate)))
                {
                    timeSlots.Insert(i,ts);
                    continue;
                }
                if ( dayWorkingHourses.FirstOrDefault( workingHours => workingHours.DayOfWeek == (i+8) % 7) == null )
                {
                    timeSlots.Insert(i,ts); 
                    continue;
                }
                int dayOfWeek = (int) tempDate.DayOfWeek;

                DayWorkingHours dwh = dayWorkingHourses.FirstOrDefault(wh => wh.DayOfWeek == dayOfWeek);

                List<Booking> dayBookings = bookings.Where(b => b.DateTime.Date == tempDate.Date).ToList();
                DayAgendaService dayAgendaService = _dayAgendasManager.GetDayAgenda(
                     tempDate,
                     dwh,
                     dayBookings);

                List<ITimeSlot> timeS = dayAgendaService.GetAllAvailableTimeSlots();
                ts.AddRange(timeS);
                timeSlots.Insert(i,ts);
            }
            
            return timeSlots;
        }


        private DateTime FirstDateOfWeekISO8601(int year, int weekOfYear)
        {
            DateTime jan1 = new DateTime(year, 1, 1);
            int daysOffset = DayOfWeek.Thursday - jan1.DayOfWeek;

            DateTime firstThursday = jan1.AddDays(daysOffset);
            var cal = CultureInfo.CurrentCulture.Calendar;
            int firstWeek = cal.GetWeekOfYear(firstThursday, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);

            var weekNum = weekOfYear;
            if (firstWeek <= 1)
            {
                weekNum -= 1;
            }
            var result = firstThursday.AddDays(weekNum * 7);
            return result.AddDays(-3);
        }
    }
}
