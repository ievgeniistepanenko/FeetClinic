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
        private readonly List<IDayAgenda> _agendas;

        public MonthScheduler(int number, List<DateTime> workingDays, List<DateTime> holidayDays, List<IDayAgenda> agendas)
        {
            
            Month = number;
            WorkingDays = workingDays;
            HolidayDays = holidayDays;
            _agendas = agendas;
            ValidateIncomingData(workingDays, holidayDays, agendas);
        }

        public List<DateTime> GetAllWorkingDays()
        {
            return WorkingDays;
        }

        public List<DateTime> GetAllHolidayDays()
        {
            return HolidayDays;
        }
        public IDayAgenda GetAgenda(DateTime date)
        {
            ValidateMonth(date);
            return _agendas.Find(da => da.Date.Day == date.Day);
        }

        public List<IDayAgenda> GetAgendas(DateTime startDate, DateTime endDate)
        {
            ValidateMonth(startDate);
            ValidateMonth(endDate);
            List<IDayAgenda> agendas = new List<IDayAgenda>();
            agendas.AddRange(_agendas.Where(dayAgenda =>
                                                    dayAgenda.Date.Day >= startDate.Day &&
                                                    dayAgenda.Date.Day <= endDate.Day));
            return agendas;
        }

        public List<IDayAgenda> GetAgendas(DateTime startDate)
        {
            ValidateMonth(startDate);
            List<IDayAgenda> agendas = new List<IDayAgenda>();
            agendas.AddRange(_agendas.Where(dayAgenda =>
                                             dayAgenda.Date.Day >= startDate.Day));
            return agendas;
        }

        public List<IDayAgenda> GetAgendas(List<DateTime> dates)
        {
            List<IDayAgenda> agendas = new List<IDayAgenda>();
            agendas.AddRange(_agendas.Where(agenda => dates.Any(d => d.Date == agenda.Date)));
            return agendas;
        }

        public List<IDayAgenda> GetAgendas()
        {
            return _agendas;
        }

        private void ValidateIncomingData(List<DateTime> workingDays, List<DateTime> holidayDays, List<IDayAgenda> agendas)
        {
            if (workingDays == null || holidayDays == null || agendas == null)
            {
                throw new ArgumentException("Can not create scheduler with give parameter");
            }
                
            if (workingDays.Any(wd => holidayDays.Any( hd => hd.Date == wd.Date) ))
            {
                throw new ArgumentException("Can not create scheduler with given parameter");
            }
            if (agendas.Any(agenda => holidayDays.Any( d => d.Date == agenda.Date)))
            {
                throw new ArgumentException("Can not create scheduler with given parameter");
            }
            if(workingDays.Any(wd => wd.Month != Month))
            {
                throw new ArgumentException("Can not create scheduler with given parameter");
            }
            if (holidayDays.Any(hd => hd.Month != Month))
            {
                throw new ArgumentException("Can not create scheduler with given parameter");
            }
            if(agendas.Any(da => da.Date.Month != Month))
            {
                throw new ArgumentException("Can not create scheduler with given parameter");
            }
        }
        private void ValidateMonth(DateTime date)
        {
            if (date.Month != Month)
            {
                throw new ArgumentException("Wrong month");
            }
        }
    }
}
