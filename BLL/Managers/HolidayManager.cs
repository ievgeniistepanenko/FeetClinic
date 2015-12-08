using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE.BE.Schedule;
using FeetClinic_DAL.Abstarct;

namespace BLL.Managers
{
    public class HolidayManager : AbstractManager<Holiday>
    {
        public HolidayManager()
        {
            Repository = Facade.Holidays;
        }
        protected override IRepository<Holiday> GetRepository()
        {
            return Repository;
        }

        //public List<Holiday> GetAllForTherapist(int therapistId)
        //{
        //    return Repository.GetAll(h => h.TherapistId == therapistId).ToList();
        //}
    }
}
