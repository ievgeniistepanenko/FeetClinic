using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE.BE.Schedule;
using DomainModel.BE.Schedule;
using FeetClinic_DAL.Abstarct;
using FeetClinic_DAL.Conrete;

namespace BLL.Managers
{
    public class DayWorkingHoursManager 
        //: AbstractManager<DayWorkingHours>
    {
        private DalFacade facade;
        public DayWorkingHoursManager()
        {
            facade = new DalFacade();
        }

        public IEnumerable<DayWorkingHours> GetAllWorkingHours(int therapistId)
        {
            return facade.WorkingHours.GetAll(wh => wh.TherapistId == therapistId);
        }

        public DayWorkingHours GetWorkingHours(int therapistId, int dayOfWeek)
        {
            return facade.WorkingHours.GetOne(wh => wh.TherapistId == therapistId && wh.DayOfWeek == dayOfWeek);
        } 

    }
}
