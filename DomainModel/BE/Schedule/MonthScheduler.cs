using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel.Interfaces;

namespace DomainModel.BE.Schedule
{
    public class MonthScheduler : IMonthScheduler
    {
        public int Month { get; set; }
        public List<DateTime> WorkingDays { get; set; }
        public List<DateTime> HolidayDays { get; set; }
        private readonly List<IDayAgenda> Agendas;

        public MonthScheduler(int number, List<DateTime> workingDays, List<DateTime> holidayDays, List<IDayAgenda> agendas)
        {
            Month = number;
            WorkingDays = workingDays;
            HolidayDays = holidayDays;
            Agendas = agendas;
        }


        public List<DateTime> GetAllWorkingDays()
        {
            return WorkingDays;
        }

        public List<DateTime> GetAllHolidaygDays()
        {
            return HolidayDays;
        }
        public IDayAgenda GetAgenda(DateTime date)
        {
            ValidateMonth(date);
            return Agendas.Find(da => da.Date.Day == date.Day);
        }

        private void ValidateMonth(DateTime date)
        {
            if (date.Month != Month)
            {
                throw new ArgumentException("Wrong month");
            }
        }

        public List<IDayAgenda> GetAgendas(DateTime startDate, DateTime endDate)
        {
            ValidateMonth(startDate);
            ValidateMonth(endDate);
            return (List<IDayAgenda>) Agendas.Where(dayAgenda => 
                                                    dayAgenda.Date.Day >= startDate.Day &&
                                                    dayAgenda.Date.Day <= endDate.Day);
        }

        public List<IDayAgenda> GetAgendas(DateTime startDate)
        {
            ValidateMonth(startDate);
            return (List<IDayAgenda>)Agendas.Where(dayAgenda =>
                                             dayAgenda.Date.Day >= startDate.Day);
        }

        public List<IDayAgenda> GetAgendas()
        {
            return Agendas;
        }
    }
}
