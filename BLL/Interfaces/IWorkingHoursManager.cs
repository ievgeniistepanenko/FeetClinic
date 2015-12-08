using System;
using System.Collections.Generic;
using BE.Interfaces;

namespace BLL.Interfaces
{
    public interface IWorkingHoursManager
    {
        IDayWorkingHours GetDayWorkingHours(DateTime date);
        List<IDayWorkingHours> GetPeriodsWorkingHourses(DateTime starDate, DateTime endDate);

    }
}
