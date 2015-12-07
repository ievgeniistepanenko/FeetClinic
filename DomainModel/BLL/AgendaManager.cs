using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel.BE.Schedule;
using DomainModel.BLL.Interfaces;
using DomainModel.Interfaces;

namespace DomainModel.BLL
{
    public class AgendaManager : IAgendaManager
    {
        private readonly IWorkingHoursManager _workingHoursManagerManager;

        public AgendaManager(IWorkingHoursManager _workingHoursManagerManager)
        {
            this._workingHoursManagerManager = _workingHoursManagerManager;
        }

        public IDayAgenda GetAgenda(DateTime date)
        {
            return new DayAgenda(date, _workingHoursManagerManager.GetDayWorkingHours(date));
        }

        public List<IDayAgenda> GetAgendas(DateTime startDate, DateTime endDate)
        {
            throw new NotImplementedException();
        }

        public List<IDayAgenda> GetAgendas(List<DateTime> dates)
        {
            throw new NotImplementedException();
        }
    }
}
