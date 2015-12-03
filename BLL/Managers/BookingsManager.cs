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
    }
}
