using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE.BE;
using BE.BE.Schedule;
using DomainModel.BE;
using DomainModel.BE.Schedule;
using FeetClinic_DAL.Abstarct;

namespace BLL.Managers
{
    public class BookingsManager : AbstractManager<Booking>
    {
        private DayWorkingHoursManager _whManager;
        private DayAgendasManager _dayAgendasManager;
        public BookingsManager()
        {
            Repository = Facade.Bookings;
        }

        protected override IRepository<Booking> GetRepository()
        {
            return Repository;
        }

        public IEnumerable<Booking> GetAllForCustomer(int customerId)
        {
            return Repository.GetAll(b=>b.CustomerProfileId == customerId,
                                        b => b.OrderBy(bok => bok.DateTime),
                                        "Treatments");
        }

        public IEnumerable<Booking> GetAllForTherapist(int therapistId, int day, int year)
        {

            return Repository.GetAll(b => b.TherapistId == therapistId && b.DateTime.DayOfYear == day,
                                     b => b.OrderBy(bok => bok.DateTime),
                                     "Treatments");
        }
          
        public override IEnumerable<Booking> GetAll()
        {
            throw new InvalidOperationException();
        }

        public override IEnumerable<Booking> GetAll(string properties)
        {
            return GetAll();
        }


        public override Booking Update(Booking entity)
        {

            throw new InvalidOperationException();
        }

        public override Booking Create(Booking entity)
        {
            _whManager = new DayWorkingHoursManager();
            _dayAgendasManager = new DayAgendasManager();
            DayWorkingHours wh = _whManager.GetWorkingHours(entity.TherapistId, 
                (int) entity.DateTime.DayOfWeek);

            List<Booking> bookings = GetAllForTherapist(entity.TherapistId, entity.DateTime.DayOfYear,
                entity.DateTime.Year).ToList();
            DayAgenda dayAgenda =  _dayAgendasManager.GetDayAgenda(entity.DateTime,wh,bookings);
            if (dayAgenda.IsAvailableForBooking(entity))

            return base.Create(entity);
            throw new ArgumentException("This is no place for booking");
        }

        
    }
}
