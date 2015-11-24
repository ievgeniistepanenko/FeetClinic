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
                UpdateTimeSlots(bookings);
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

            IEnumerable<TimeSlot> allAvailableSlots = GetAllAvailableTimeSlots();
            

            

            throw new NotImplementedException();

        }
        private List<TimeSlot> CreateTimeSlots(WorkingHours workingHours)
        {
            List<TimeSlot> timeSlots = new List<TimeSlot>();
            for (int i = 3; i >= 1; i--)
            {
                TimeSpan timeSpan = new TimeSpan(0, 0, 5 * i); // 15 min, 10 min , 5 min
                TimeSlot timeSlot = new TimeSlot(0,new DateTime(2000,1,1,0,0,0),timeSpan,true );
                try
                {
                    return GetTimeSlots(timeSlot, workingHours);
                }
                catch (Exception)
                {
                    // ignored
                }

            }
            throw new ArgumentException("working hours is not valid");
        }
        private IEnumerable<TimeSlot> GetAllAvailableTimeSlots()
        {
            return TimeSlots.Where(ts => ts.IsAvailable);
        }

        //sets timeslots unavailable if some booking is cover their
        private void UpdateTimeSlots(List<Booking> bookings)
        {
            foreach (Booking booking in bookings)
            {
                TimeSlot firstTimeSlot = TimeSlots.Find(ts => ts.StartTime.TimeOfDay == booking.DateTime.TimeOfDay);
                firstTimeSlot.IsAvailable = false;
                int amounTimeSlots = firstTimeSlot.GetSlotsAmount( booking.GetDuration() );

                for (int i = 1; i < amounTimeSlots; i++)
                {
                    TimeSlot timeSlot = TimeSlots.FirstOrDefault(ts => ts.Number == firstTimeSlot.Number + i);

                    if (timeSlot != null) timeSlot.IsAvailable = false;
                }
            }
        }

        private List<TimeSlot> GetTimeSlots(TimeSlot timeSlot, WorkingHours workingHours)
        {
            List<TimeSlot> timeSlotses = new List<TimeSlot>();
            int amounOfTimeSlot = timeSlot.GetSlotsAmount(workingHours);
            //for (int i = 0; i < amounOfTimeSlot; i++)
            //{
            //    timeSlotses.Add(new TimeSlot(i+1,,timeSlot.Duration.Duration(),true));
            //}

            //DateTime temp = workingHours.StartTime;
            //int numberOfTimeSlot = 1;

            //do
            //{
            //    timeSlotses.Add(new TimeSlot(numberOfTimeSlot, temp, timeSpan, true));
            //    temp = temp + timeSpan.Duration();
            //    numberOfTimeSlot++;

            //} while (temp.TimeOfDay < workingHours.StartLunch.TimeOfDay);

            //if (!temp.TimeOfDay.Equals(workingHours.StartLunch.TimeOfDay)) throw new ArgumentException();

            //temp = temp + workingHours.LunchDuration.Duration();

            //do
            //{
            //    timeSlotses.Add(new TimeSlot(numberOfTimeSlot, temp, timeSpan, true));
            //    temp = temp + timeSpan.Duration();
            //    numberOfTimeSlot++;
            //} while (temp.TimeOfDay < workingHours.EndTime.TimeOfDay);

            //if (!temp.TimeOfDay.Equals(workingHours.EndTime.TimeOfDay)) throw new ArgumentException();

            return timeSlotses;

        }
    }
}
