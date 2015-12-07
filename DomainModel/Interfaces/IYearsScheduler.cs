using System;
using System.Collections.Generic;

namespace BE.Interfaces
{
    public interface IYearsScheduler
    {
        int Year { get; }
        IMonthScheduler GetMonthScheduler(int month);
        List<DateTime> GetAllWorkingDays(DateTime startDate, DateTime endDate);
        List<DateTime> GetAllHolidaygDays(DateTime startDate, DateTime endDate);
        void AddHolidayDay(DateTime date);
        void AddHolidayDays(DateTime startDate, DateTime endDate);
        IDayAgenda GetAgenda(DateTime date);
        List<IDayAgenda> GetAgendas(DateTime startDate, DateTime endDate);
    }
}
