using System;
using System.Collections.Generic;
using BE.Interfaces;

namespace DomainModel.BLL.Interfaces
{
    public interface IAgendaManager
    {
        IDayAgenda GetAgenda(DateTime date);
        List<IDayAgenda> GetAgendas(DateTime startDate, DateTime endDate);
        List<IDayAgenda> GetAgendas(List<DateTime> dates);
    }
}
