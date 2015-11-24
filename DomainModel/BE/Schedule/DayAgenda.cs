using System;
using System.Collections.Generic;
using System.Linq;
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
            TimeSlots = CreateTimeSlots(wh);
            if (bookings != null)
            {
                Bookings = bookings;
                UpdateTimeSlotsWithBookings(bookings);
            }
        }

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

        public bool IsAvailableForBooking(Booking booking)
        {
            return GetAvailableTimeSlots(booking).Any();
        }

        public IEnumerable<TimeSlot> GetAvailableTimeSlots(Booking booking)
        {
            List<TimeSlot> availableTimeSlots = new List<TimeSlot>();

            int necessaryTimeSlots = ComputeBookingsTimeSlotsAmount(booking, TimeSlots.First());
            IEnumerable<TimeSlot> allAvailableSlots = GetAllAvailableTimeSlots();
            

            

            throw new NotImplementedException();

        }

        private IEnumerable<TimeSlot> GetAllAvailableTimeSlots()
        {
            return TimeSlots.Where(ts => ts.IsAvailable);
        } 
        private void UpdateTimeSlotsWithBookings(List<Booking> bookings)
        {
            foreach (Booking booking in bookings)
            {
                TimeSlot firstTimeSlot = TimeSlots.Find(ts => ts.StartTime.TimeOfDay == booking.DateTime.TimeOfDay);
                firstTimeSlot.IsAvailable = false;
                int amounTimeSlots = ComputeBookingsTimeSlotsAmount(booking, firstTimeSlot);

                for (int i = 1; i < amounTimeSlots; i++)
                {
                    TimeSlot timeSlot = TimeSlots.FirstOrDefault(ts => ts.Number == firstTimeSlot.Number + i);
                    timeSlot.IsAvailable = false;
                }
            }
        }

        private int ComputeBookingsTimeSlotsAmount(Booking booking, TimeSlot timeSlot)
        {
            TimeSpan bookingTimeSpan = new TimeSpan(0, 0, 0, 0, 0);
            foreach (Treatment treatment in booking.Treatments)
            {
                bookingTimeSpan = bookingTimeSpan + treatment.Duration.Duration();
            }
            TimeSpan timeSlotDuration = timeSlot.Duration.Duration();
            int amount = 0;
            do
            {
                bookingTimeSpan = bookingTimeSpan - timeSlotDuration;
                amount++;
            } while (bookingTimeSpan > timeSlotDuration);

            return amount;

        }
        private List<TimeSlot> CreateTimeSlots(WorkingHours workingHours)
        {
            List<TimeSlot> timeSlots = new List<TimeSlot>();
            for (int i = 3; i >= 1; i--)
            {
                TimeSpan timeSpan = new TimeSpan(0, 0, 5 * i); // 15 min, 10 min , 5 min
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
                timeSlotses.Add(new TimeSlot(numberOfTimeSlot, temp, timeSpan, true));
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
    }
}
