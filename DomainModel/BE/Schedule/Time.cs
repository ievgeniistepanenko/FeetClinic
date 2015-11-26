using System;

namespace DomainModel.BE.Schedule
{
    public struct Time : IComparable<Time>
    {
        private DateTime _time;
        public int Hour
        {
            get { return _time.Hour; }
            private set { value = Hour; }
        }

        public int Minute
        {
            get { return _time.Minute; }
            private set { value = Minute; }
        }

        public Time (TimeSpan timeSpan) : this(timeSpan.Hours, timeSpan.Minutes) { }
        public Time(DateTime dateTime) : this(dateTime.Hour,dateTime.Minute){}
        public Time(int hour, int minute)
        {
            if ( hour <0 || hour >23 || minute > 59 || minute < 0)
            {
                throw new ArgumentException("Invalid time parameter");
            }
            _time = new DateTime(100,1,1,hour,minute,0);
            Hour = _time.Hour;
            Minute = _time.Minute;
            
        }

        public Time AddHours(int hours)
        {
            return new Time(_time.AddHours(hours));
        }

        public Time AddMinutes(int minutes)
        {
            return new Time(_time.AddMinutes(minutes));
        }

        public Time Add(Time time)
        {
            return new Time(_time.AddHours(time.Hour).AddMinutes(time.Minute));
        }

        public Time Add(TimeSpan timeSpan)
        {
            DateTime dateTime = _time.Add(timeSpan);
            Time newTime = new Time(_time.Add(timeSpan));
            return newTime;
        }

        public Time Subtract(Time time)
        {
            TimeSpan timeSpan = new TimeSpan(time.Hour,time.Minute,0);
            return Subtract(timeSpan);
        }

        public Time Subtract(TimeSpan timeSpan)
        {
            TimeSpan ts = new TimeSpan(timeSpan.Days,timeSpan.Hours,timeSpan.Minutes,0); 
            return new Time(_time.Subtract(ts));
        }

        public TimeSpan GetAbsoluteDifference(Time time)
        {
            return _time.Subtract(time._time).Duration();
        }

        public static bool operator <(Time t1, Time t2)
        {
            return t1._time < t2._time;
        }
        public static bool operator >(Time t1, Time t2)
        {
            return t1._time > t2._time;
        }
        public static bool operator <=(Time t1, Time t2)
        {
            return t1._time <= t2._time;
        }
        public static bool operator >=(Time t1, Time t2)
        {
            return t1._time >= t2._time;
        }

        public static bool operator <(Time t1, TimeSpan timeSpan)
        {
            return t1 < new Time(timeSpan);
        }
        public static bool operator >(Time t1, TimeSpan timeSpan)
        {
            return t1 > new Time(timeSpan);
        }
        public static bool operator <=(Time t1, TimeSpan timeSpan)
        {
            return t1 <= new Time(timeSpan);
        }
        public static bool operator >=(Time t1, TimeSpan timeSpan)
        {
            return t1 >= new Time(timeSpan);
        }
        public static bool operator ==(Time t1, TimeSpan timeSpan)
        {
            return t1.Equals(new Time(timeSpan)) ;
        }
        public static bool operator !=(Time t1, TimeSpan timeSpan)
        {
            return !t1.Equals(new Time(timeSpan));
        }
        public bool Equals(Time time)
        {
            return time.Hour == Hour && time.Minute == Minute;
        }

        public int CompareTo(Time other)
        {
            if (this > other)
            {
                return 1;
            }
            if (this < other)
            {
                return -1;
            }
            return 0;
        }

        public override bool Equals(Object obj)
        {
            Time time = (Time) obj;
            return time.Hour == Hour && time.Minute == Minute;
        }

    }
}
