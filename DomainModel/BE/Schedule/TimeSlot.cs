using System;
using System.Collections.Generic;
using System.Linq;
using BE.Interfaces;

namespace BE.BE.Schedule
{
    public class  TimeSlot : ITimeSlot, IEntity
    {
        public int Number { get; }
        public Time StartTime { get; set; }
        public TimeSpan Duration { get;  }
        public bool IsAvailable { get; set; }

        public TimeSlot(int number, Time startTime, TimeSpan duration, bool isAvailable)
        {
            Number = number;
            StartTime = startTime;
            Duration = duration;
            IsAvailable = isAvailable;
        }

        public int GetSlotsAmount(TimeSpan timeSpan)
        { 
            int count = 0;
            TimeSpan tempSpan = new TimeSpan(0,0,0,0);
            do
            {
                tempSpan = tempSpan.Add(Duration);
                count++;
            } while (tempSpan < timeSpan);
            return count;
        }

        public int GetSlotsAmount(IDayWorkingHours dayWorkingHours)
        {
            List<ITimeSlot> timeSlots = dayWorkingHours.GetWorkingHours();
            Time minTime = timeSlots.Min(ts => ts.StartTime);
            Time maxTime = timeSlots.Max(ts => ts.StartTime.Add( ts.Duration ) );
            return GetSlotsAmount(minTime.GetAbsoluteDifference(maxTime));
        }

        public int Id { get; set; }
    }

}
