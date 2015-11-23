using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.BE.Schedule
{
    public struct  TimeSlot
    {
        public int Number { get; }
        public DateTime StartTime { get;  }
        public TimeSpan Duration { get;  }
        public bool IsAvailable { get;  }

        public TimeSlot(int number, DateTime startTime, TimeSpan duration, bool isAvailable)
        {
            Number = number;
            StartTime = startTime;
            Duration = duration;
            IsAvailable = isAvailable;
        }
    }

}
