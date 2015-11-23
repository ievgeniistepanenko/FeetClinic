using System;
using System.Collections.Generic;
using DomainModel.BE.Schedule;
using DomainModel.Interfaces;

namespace DomainModel.BE.Scheduler
{
    public class DayAgenda : IDayAgenda
    {
        public DateTime Date { get; }
        public List<Booking> Bookings { get; }
        public List<TimeSlot> TimeSlots { get; } 

        public DayAgenda(DateTime date, WorkingHours wh,List<Booking> bookings = null  )
        {
            Date = date;
            TimeSlots = ComputeTimeSlots(wh);
            if (bookings != null)
            {
                Bookings = bookings;
            }
        }
        private void UpdateTimeSlotsWithBookings(List<Booking> bookings)
        {
            foreach (Booking booking in bookings)
            {
                TimeSlot timeSlot = TimeSlots.Find(ts => ts.StartTime.TimeOfDay == booking.DateTime.TimeOfDay);

            }
        }

        private int ComputeBookingsTimeSlotsAmount(Booking booking, TimeSlot timeSlot)
        {
            TimeSpan bookingTimeSpan = new TimeSpan(0,0,0,0,0);
            int amount = 0;
            foreach (Treatment treatment in booking.Treatments)
            {
                bookingTimeSpan = bookingTimeSpan + treatment.Duration.Duration();
            }
            TimeSpan tempTimeSpan = new TimeSpan(0,0,0,0,0);
            do
            {
                amount++;
                bookingTimeSpan = bookingTimeSpan - timeSlot.Duration.Duration();
            } while (bookingTimeSpan.TotalMinutes > 0 );

            return amount;

        }
        private List<TimeSlot> ComputeTimeSlots(WorkingHours workingHours)
        {
            List<TimeSlot> timeSlots = new List<TimeSlot>();
            for (int i = 3; i >= 1; i--)
            {
                TimeSpan timeSpan = new TimeSpan(0,0,5 * i); // 15 min, 10 min , 5 min

                try
                {
                    return GetTimeSlots(timeSpan, workingHours);
                }
                catch (Exception)
                {
                    // ignored
                }
                
            }
            throw new ArgumentException("working hours is not valid");
        }

        private List<TimeSlot> GetTimeSlots(TimeSpan timeSpan, WorkingHours workingHours)
        {
            List<TimeSlot> timeSlotses = new List<TimeSlot>();
            DateTime temp = workingHours.StartTime;
            int numberOfTimeSlot = 1;

            do
            {
                timeSlotses.Add( new TimeSlot(numberOfTimeSlot,temp,timeSpan,true));
                temp = temp + timeSpan.Duration();
                numberOfTimeSlot++;

            } while (temp.TimeOfDay < workingHours.StartLunch.TimeOfDay);

            if (!temp.TimeOfDay.Equals(workingHours.StartLunch.TimeOfDay)) throw new ArgumentException();

            temp = temp + workingHours.LunchDuration.Duration();

            do
            {
                timeSlotses.Add(new TimeSlot(numberOfTimeSlot, temp, timeSpan, true));
                temp = temp + timeSpan.Duration();
                numberOfTimeSlot++;
            } while (temp.TimeOfDay < workingHours.EndTime.TimeOfDay);

            if (!temp.TimeOfDay.Equals(workingHours.EndTime.TimeOfDay)) throw new ArgumentException();

            return timeSlotses;

        }

        //private bool IsTimeSpanCompatibleWithWorkingHours(TimeSpan timeSpan, WorkingHours workingHours)
        //{
        //    DateTime temp = workingHours.StartTime;
        //    do
        //    {
        //        temp = temp + timeSpan.Duration();

        //    } while (temp.TimeOfDay < workingHours.StartLunch.TimeOfDay);

        //    if (!temp.TimeOfDay.Equals(workingHours.StartLunch.TimeOfDay)) return false;
        //    temp = temp + workingHours.LunchDuration.Duration();
        //    do
        //    {
        //        temp = temp + timeSpan.Duration();
        //    } while (temp.TimeOfDay < workingHours.EndTime.TimeOfDay);

        //    return temp.TimeOfDay.Equals(workingHours.EndTime.TimeOfDay);
        //}

        public Booking AddBooking(Booking booking)
        {
            throw new NotImplementedException();
        }

        public Booking RemoveBooking(Booking booking)
        {
            throw new NotImplementedException();
        }

        public Booking RemoveBooking(int bookingId)
        {
            throw new NotImplementedException();
        }

        public bool IsPlaceForBooking(Booking booking)
        {
            throw new NotImplementedException();
        }

        public List<TimeSlot> GetAvailableTimeSlots(Booking booking)
        {
            throw new NotImplementedException();
        }
    }
}
