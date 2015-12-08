using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE.BE.Schedule;
using FeetClinic_DAL.Abstarct;
using FeetClinic_DAL.Conrete;

namespace BLL.Managers
{
    public class DayWorkingHoursManager 
        //: AbstractManager<DayWorkingHours>
    {
        private readonly DalFacade _facade;
        public DayWorkingHoursManager()
        {
            _facade = new DalFacade();
        }

        public IEnumerable<DayWorkingHours> GetAllWorkingHours(int therapistId)
        {
            return _facade.WorkingHours.GetAll(wh => wh.TherapistId == therapistId);
        }

        public DayWorkingHours GetWorkingHours(int therapistId, int dayOfWeek)
        {
            return _facade.WorkingHours.GetOne(wh => wh.TherapistId == therapistId && wh.DayOfWeek == dayOfWeek);
        } 

    }
}
