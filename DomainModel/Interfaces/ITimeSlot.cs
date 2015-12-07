using System;
using BE.BE.Schedule;

namespace BE.Interfaces
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
