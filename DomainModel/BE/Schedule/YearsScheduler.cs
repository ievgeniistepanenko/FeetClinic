using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel.Interfaces;

namespace DomainModel.BE.Schedule
{
    public class YearsScheduler : IYearsScheduler
    {
        public int Year;
        private List<IMonthScheduler> monthSchedulers;
        private List<DateTime> Holidays;
        private List<DateTime> WorkingDays; 
         
        public IMonthScheduler GetMonthScheduler(int month)
        {
            ValidateMonth(month);
            return monthSchedulers[month - 1];
        }

        private static void ValidateMonth(int month)
        {
            if (month < 1 || month > 12)
            {
                throw new ArgumentException("Invalid month number");
            }
        }

        public List<DateTime> GetAllWorkingDays(DateTime startDate, DateTime endDate)
        {
            ValidateYear(startDate,endDate);
            return (List<DateTime>) WorkingDays.Where( day => day.Date >= startDate.Date && day.Date <= endDate.Date);
        }

        public List<DateTime> GetAllHolidaygDays(DateTime startDate, DateTime endDate)
        {
            ValidateYear(startDate, endDate);
            return (List<DateTime>) Holidays.Where(day => day.Date >= startDate.Date && day.Date <= endDate.Date);
        }

       

        public void AddHolidayDay(DateTime date)
        {
            ValidateYear(date);
            Holidays.Add(date);
        }

        public void AddHolidayDays(DateTime startDate, DateTime endDate)
        {
            ValidateYear(startDate,endDate);
            List<DateTime> holidays = new List<DateTime>();
            DateTime tempDate = startDate;
            while (startDate.Date <= endDate.Date)
            {
                holidays.Add(tempDate);
                tempDate = tempDate.AddDays(1);
            }
            Holidays.AddRange(holidays);


        }

        public IDayAgenda GetAgenda(DateTime date)
        {
            ValidateYear(date);
            ValidateMonth(date.Month);
            return monthSchedulers.Find(m => m.Month == date.Month)
                    .GetAgenda(date);
        }

        public List<IDayAgenda> GetAgendas(DateTime startDate, DateTime endDate)
        {
            ValidateYear(startDate, endDate);
            ValidateMonth(startDate.Month);
            ValidateMonth(endDate.Month);

            List<IDayAgenda> agendas = new List<IDayAgenda>();
            if (startDate.Month == endDate.Month)
            {
                return monthSchedulers.Find(m => m.Month == startDate.Month)
                        .GetAgendas(startDate, endDate);
            }
            else
            {
                IMonthScheduler ms = monthSchedulers.Find(m => m.Month == startDate.Month);
                agendas.AddRange(ms.GetAgendas(startDate));

                ms = monthSchedulers.Find(m => m.Month == endDate.Month);
                agendas.AddRange( ms.GetAgendas().Except(   ms.GetAgendas(endDate)  ));
                return agendas;
            }



        }
        private void ValidateYear(DateTime startDate, DateTime endDate)
        {
            if (startDate.Year != Year || endDate.Year != Year)
            {
                throw new ArgumentException("Invalid date");
            }
        }
        private void ValidateYear(DateTime date)
        {
            ValidateYear(date,date);
        }
    }
}
