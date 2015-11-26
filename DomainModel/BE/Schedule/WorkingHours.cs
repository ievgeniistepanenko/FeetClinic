using System;
using System.Collections.Generic;
using DomainModel.Interfaces;

namespace DomainModel.BE.Schedule
{
    public class WorkingHours : IWorkingHours
    {
        public DateTime StartTime { get; }
        public DateTime EndTime { get; }
        public DateTime StartLunch { get; }
        public TimeSpan LunchDuration { get; }

        public WorkingHours(DateTime startTime, DateTime endTime, DateTime startLunch, TimeSpan lunchDuration)
        {
            StartTime = startTime;
            EndTime = endTime;
            StartLunch = startLunch;
            LunchDuration = lunchDuration;
            if (!Validate(this))
            {
                throw new ArgumentException("Not possible create working ours");
            }
        }

        private bool Validate(WorkingHours wh)
        {
            if (!((wh.StartTime.TimeOfDay < wh.StartLunch.TimeOfDay)
                && ((wh.StartLunch.TimeOfDay + wh.LunchDuration < wh.EndTime.TimeOfDay))))
            {
                return false;
            }
            if (!((wh.StartTime.Minute % 5 == 0)
                && (wh.StartLunch.Minute % 5 == 0)
                && (wh.EndTime.Minute % 5 == 0)
                && (wh.LunchDuration.Minutes % 5 == 0)))
            {
                return false;
            }
            return true;

        }
        public List<ITimeSlot> GetWorkingHours()
        {
            List<ITimeSlot> workingHours = new List<ITimeSlot>();
            return workingHours;
        }
    }
}
