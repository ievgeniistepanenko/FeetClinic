using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel.BE;
using FeetClinic_DAL.Abstarct;

namespace BLL.Managers
{
    public class BookingsManager : AbstractManager<Booking>
    {
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

            return base.Update(entity);
        }

        public override Booking Create(Booking entity)
        {
            return base.Create(entity);
        }

        public override Booking Delete(Booking entity)
        {
            return base.Delete(entity);
        }
    }
}
