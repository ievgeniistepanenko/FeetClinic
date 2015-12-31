using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE.BE;
using BE.BE.Schedule;
using BE.BE.Treatments;
using FeetClinic_DAL.Abstarct;

namespace BLL.Managers
{
    public class BookingsManager : AbstractManager<Booking>
    {
        private DayWorkingHoursManager _whManager;
        private DayAgendasManager _dayAgendasManager;
        private HolidayManager _holidayManager;
        public BookingsManager()
        {
            Repository = Facade.Bookings;
        }

        protected override IRepository<Booking> GetRepository()
        {
            return Repository;
        }

        public IEnumerable<Booking> GetAllForCustomer(int customerId, string properties)
        {
            DateTime oneYearAgo = DateTime.Now.Subtract(new TimeSpan(365, 0, 0, 0));
            return Repository.GetAll(b=>b.CustomerProfileId == customerId &&
                                        b.DateTime >= oneYearAgo,
                                        b => b.OrderBy(bok => bok.DateTime),
                                        properties);
        }

        public IEnumerable<Booking> GetAllForTherapistForWeek(int therapistId, int week, int year, string properties)
        {
            DateTime firsDateOfWeekOfYear = FirstDateOfWeekISO8601(year,week);
            DateTime lastDateOfWeekOfYear = firsDateOfWeekOfYear.AddDays(6);

            return Repository.GetAll(b => b.TherapistId == therapistId && 
                                          b.DateTime >= firsDateOfWeekOfYear && 
                                          b.DateTime <= lastDateOfWeekOfYear,
                                     b => b.OrderBy(bok => bok.DateTime),
                                     properties);
        }
        public IEnumerable<Booking> GetAllForTherapistForDay(int therapistId, int dayOfYear, int year, string properties )
        {
            IEnumerable<Booking> bookings = Repository.GetAll(b=> b.TherapistId == therapistId,b=>b.OrderBy(bok=>bok.Id),properties);
            return bookings.Where(b => b.DateTime.DayOfYear == dayOfYear && b.DateTime.Year == year);
        }

        public override IEnumerable<Booking> GetAll()
        {
            throw new InvalidOperationException();
        }

        public override IEnumerable<Booking> GetAll(string properties)
        {
            return GetAll();
        }

        //throws Exception
        public override Booking Update(Booking entity)
        {
            throw new InvalidOperationException();
        }

        public override Booking Create(Booking entity)
        {
            _holidayManager = new HolidayManager();
            _whManager = new DayWorkingHoursManager();
            _dayAgendasManager = new DayAgendasManager();
            DayWorkingHours wh;

            List<Holiday> holidaysForTherapist = _holidayManager.GetAllForTherapist(entity.TherapistId);
            if (holidaysForTherapist.Any(holiday => holiday.IsHoliday(entity.DateTime)))
            {
                throw new ArgumentException("This is holiday day");
            }

            try
            {
                wh = _whManager.GetWorkingHours(entity.TherapistId,
                (int)entity.DateTime.DayOfWeek);
            }
            catch (Exception)
            {
                
                throw new Exception("Opening hours for given day of week not exist");
            }
            

            List<Booking> bookings = GetAllForTherapistForDay(entity.TherapistId, entity.DateTime.DayOfYear,
                entity.DateTime.Year,"").ToList();

            DayAgendaService dayAgendaService =  _dayAgendasManager.GetDayAgenda(entity.DateTime,wh,bookings);

            if (dayAgendaService.CanAddBookingWithGivenTime(entity))
            {
                if (entity.CustomerProfile != null)
                {
                    Facade.CustomerProfiles.AttachIfDetached(entity.CustomerProfile);
                }
                if (entity.Therapist != null)
                {
                    Facade.Therapist.AttachIfDetached(entity.Therapist);
                }
                if (entity.Treatments != null && entity.Treatments.Any())
                {
                    foreach (Treatment treatment in entity.Treatments)
                    {
                        Facade.Treatments.AttachIfDetached(treatment);
                    }
                }
                return base.Create(entity);
            }
            

            throw new ArgumentException("This is no place for booking");
        }


        private  DateTime FirstDateOfWeekISO8601(int year, int weekOfYear)
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
