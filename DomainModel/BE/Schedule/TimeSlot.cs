using System;

namespace DomainModel.BE.Schedule
{
    public class  TimeSlot
    {
        public int Number { get; }
        public DateTime StartTime { get; set; }
        public TimeSpan Duration { get;  }
        public bool IsAvailable { get; set; }

        public TimeSlot(int number, DateTime startTime, TimeSpan duration, bool isAvailable)
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

        public int GetSlotsAmount(WorkingHours workingHours)
        {
            return GetSlotsAmount(workingHours.EndTime - workingHours.StartTime);
        }
    }

}
