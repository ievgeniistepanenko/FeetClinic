using System;
using System.Collections.Generic;

namespace BE.Interfaces
{
    public interface IDayWorkingHours
    {
        List<ITimeSlot> GetWorkingHours();
        TimeSpan GetWorkDayDuration();
    }
}
