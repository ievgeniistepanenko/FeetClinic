using System;
using System.Collections.Generic;
using DomainModel.Interfaces;

namespace DomainModel.BE.Schedule
{
    public class DayWorkingHours : IDayWorkingHours
    {
        public int DayOfWeek { get; set; }
        public Time StartTime { get; }
        public Time EndTime { get; }
        public Time StartLunch { get; }
        public TimeSpan LunchDuration { get; }
        

        public DayWorkingHours(Time startTime, Time endTime, Time startLunch, TimeSpan lunchDuration)
        {
            StartTime = startTime;
            EndTime = endTime;
            StartLunch = startLunch;
            LunchDuration = lunchDuration;
            if (!Validate())
            {
                throw new ArgumentException("Not possible create working ours");
            }
        }

        private bool Validate()
        {
            if (!((StartTime < StartLunch)
                && (StartLunch.Add(LunchDuration)   < EndTime)))
            {
                return false;
            }
            if (!((StartTime.Minute % 5 == 0)
                && (StartLunch.Minute % 5 == 0)
                && (EndTime.Minute % 5 == 0)
                && (LunchDuration.Minutes % 5 == 0)))
            {
                return false;
            }
            return true;
        }
        public List<ITimeSlot> GetWorkingHours()
        {
            List<ITimeSlot> workingHours = new List<ITimeSlot>
            {
                new TimeSlot(1,
                    StartTime,
                    StartLunch.GetAbsoluteDifference(StartTime),
                    true),
                new TimeSlot(2,
                    StartLunch.Add(LunchDuration),
                    EndTime.GetAbsoluteDifference(StartLunch.Add(LunchDuration)),
                    true)
            };
            return workingHours;
        }

        public TimeSpan GetWorkDayDuration()
        {
            return EndTime.GetAbsoluteDifference(StartTime);
        }
    }
}
