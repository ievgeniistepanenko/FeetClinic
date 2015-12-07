using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel.BLL;
using DomainModel.BLL.Interfaces;
using DomainModel.Interfaces;

namespace DomainModel.BE.Schedule
{
    public class YearsScheduler : IYearsScheduler
    {
        public int Id { get; set; }
        private int TherapistId;
        public int Year { get; }
        private List<IMonthScheduler> monthSchedulers;
        private readonly List<DateTime> Holidays;
        private readonly List<DateTime> WorkingDays;
        private List<DayAgenda> dayAgendas;
        private List<DayWorkingHours> workingHours;

        
        public YearsScheduler(int year)
        {
        }


        public IMonthScheduler GetMonthScheduler(int month)
        {
            ValidateMonth(month);
            //IMonthScheduler monthScheduler = 
            //    new MonthScheduler(month,
            //                       GetDaysForMonth(month, WorkingDays),
            //                       GetDaysForMonth(month, Holidays),
            //                       _agendaManager.GetAgendas( GetDaysForMonth(month, WorkingDays)) );

            //return monthScheduler;
            return monthSchedulers.Find(sc => sc.Month == month);
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

        private List<DateTime> GetDaysForMonth(int month, List<DateTime> days)
        {
            List<DateTime> daysForMonth = new List<DateTime>();
            daysForMonth.AddRange(   days.Where( date => date.Month == month));
            return daysForMonth;
        }

        private List<DateTime> GetDaysExcept (List<DateTime> dates, int year )
        {
            List<DateTime> days = new List<DateTime>();
            int daysInYear = GetDaysInTheYear(year);
            DateTime dateTime = new DateTime(year, 1, 1);
            for (int i = 0; i < daysInYear; i++)
            {
                days.Add(dateTime);
                dateTime = dateTime.AddDays(1);
            }
            return days.Except(dates).ToList();

        }

        private int GetDaysInTheYear(int year)
        {
            var thisYear = new DateTime(year,1,1);
            var nextYear = new DateTime(year+1, 1, 1);
            return (nextYear - thisYear).Days;
        }
        //public YearsScheduler(int year,List<DateTime> holidays,IWorkingHoursManager whManagerManager, 
        //                    IAgendaManager agendaManager)
        //{
        //    Year = year;
        //    Holidays = holidays;
        //    WorkingDays = GetDaysExcept(holidays,year);
        //    _workingHoursManagerManager = whManagerManager;
        //    _agendaManager = agendaManager;
        //}
    }
}
