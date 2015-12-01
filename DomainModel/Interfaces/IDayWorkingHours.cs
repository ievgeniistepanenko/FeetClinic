﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Interfaces
{
    public interface IDayWorkingHours
    {
        List<ITimeSlot> GetWorkingHours();
        TimeSpan GetWorkDayDuration();
    }
}