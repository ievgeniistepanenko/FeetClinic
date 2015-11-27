using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Interfaces
{
    public interface IMonthScheduler
    {
        int Id { get; set; }
        int Month { get; set; }
        List<DateTime> WorkingDays { get; set; }
        List<DateTime> HolidayDays { get; set;}
        IDayAgenda GetAgenda(DateTime date);
        List<IDayAgenda> GetAgendas(DateTime startDate, DateTime endDate);
        List<IDayAgenda> GetAgendas(DateTime startDate);
        List<IDayAgenda> GetAgendas(List<DateTime> dates );
        List<IDayAgenda> GetAgendas();
    }
}
