using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Interfaces
{
    public interface IScheduler
    {
        List<DateTime> GetAllWorkingDays(int month);
        List<DateTime> GetAllHolidaygDays(int month);
        List<DateTime> GetAllWorkingDays(DateTime startDate, DateTime endDate);
        List<DateTime> GetAllHolidaygDays(DateTime startDate, DateTime endDate);
        void SetHolidayDay(DateTime date);
        void SetHolidayDays(DateTime startDate, DateTime endDate);
        IDayAgenda GetAgenda(DateTime date);
        List<IDayAgenda> GetAgendas(DateTime startDate, DateTime endDate);
    }
}
