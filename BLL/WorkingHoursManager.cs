using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel.BLL.Interfaces;
using DomainModel.Interfaces;

namespace DomainModel.BE.Schedule
{
    public class WorkingHoursManager : IWorkingHoursManager
    {
        //private readonly Dictionary<DateTime, IDayWorkingHours> _specialWorkingHourses;

        private readonly Dictionary<DayOfWeek, IDayWorkingHours> _weekDayWorkingHourses;

        public WorkingHoursManager(Dictionary<DateTime, IDayWorkingHours> specialWorkingHourses, 
            Dictionary<DayOfWeek, IDayWorkingHours> weekDayWorkingHourses)
        {
            //_specialWorkingHourses = specialWorkingHourses;
            _weekDayWorkingHourses = weekDayWorkingHourses;
        }
        public WorkingHoursManager(Dictionary<DayOfWeek, IDayWorkingHours> weekDayWorkingHourses)
        {
            //_specialWorkingHourses = specialWorkingHourses;
            _weekDayWorkingHourses = weekDayWorkingHourses;
        }

        public IDayWorkingHours GetDayWorkingHours(DateTime date)
        {
            IDayWorkingHours wh;
            //if (_specialWorkingHourses.TryGetValue(date,out wh))
            //{
            //    return wh;
            //}
            wh = _weekDayWorkingHourses[date.DayOfWeek];
            return wh;
        }

        public List<IDayWorkingHours> GetPeriodsWorkingHourses(DateTime starDate, DateTime endDate)
        {
            List<IDayWorkingHours> dwh = new List<IDayWorkingHours>();
            DateTime temDate = starDate;
            while (starDate.Date <= endDate.Date)
            {
                dwh.Add( GetDayWorkingHours(temDate));
                temDate = temDate.AddDays(1);
            }
            return dwh;
        }
    }
}
