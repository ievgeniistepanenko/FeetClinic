using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.BE.Schedule
{
    public class  TimeSlot
    {
        public int Number { get; }
        public DateTime StartTime { get;  }
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
    }

}
