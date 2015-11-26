using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel.BE.Schedule;

namespace DomainModel.Interfaces
{
    public interface ITimeSlot 
    {
        int Number { get;}
        Time StartTime { get; set; }
        TimeSpan Duration { get; }
        bool IsAvailable { get; set; }
        int GetSlotsAmount(TimeSpan timeSpan);

    }
}
