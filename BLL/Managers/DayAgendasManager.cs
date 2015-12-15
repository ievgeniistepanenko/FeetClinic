using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE.BE;
using BE.BE.Schedule;
using FeetClinic_DAL.Abstarct;

namespace BLL.Managers
{
    public class DayAgendasManager 
    {

        public DayAgendaService GetDayAgenda(DateTime date, DayWorkingHours wh, List<Booking> bookings)
        {
            return new DayAgendaService(date,wh,bookings);
        }

    }
}
