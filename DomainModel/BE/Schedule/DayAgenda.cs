using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using DomainModel.BE.Schedule;
using DomainModel.Interfaces;

namespace DomainModel.BE.Scheduler
{
    public class DayAgenda : IDayAgenda
    {
        private readonly int timeSlotDurationMinute = 15; //min
        public DateTime Date { get; }
        public List<Booking> Bookings { get; }
        public List<TimeSlot> TimeSlots { get; } 

        public DayAgenda(DateTime date, WorkingHours wh,List<Booking> bookings = null  )
        {
            if (!ValidateDate(date))
            {
                throw new ArgumentException("Date must represent just date without time");
            }
            Date = date;
            TimeSlots = CreateTimeSlots(wh);

            if (bookings != null)
            {
                Bookings = bookings;
                UpdateTimeSlots(bookings);
            }
            else
            {
                Bookings = new List<Booking>();
            }
        }

        public Booking AddBooking(Booking booking)
        {
            List<TimeSlot> availableTimeSlots = GetAvailableTimeSlots(booking);
            if (!IsAvailableForBooking(booking))
            {
                throw new Exception("This is no place for booking");
            }

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

        public List<TimeSlot> GetAvailableTimeSlots(Booking booking)
        {
            List<TimeSlot> timeSlots = new List<TimeSlot>();
            List<TimeSlot> allAvailableTimeSlots = GetAllAvailableTimeSlots();
            TimeSpan bookingDuration = booking.GetDuration().Duration();

            int amount = 0;
            if (allAvailableTimeSlots.Any())
            {
                amount = allAvailableTimeSlots.First().GetSlotsAmount(bookingDuration);

                for (int i = 0; i < allAvailableTimeSlots.Count - amount; i++)
                {
                    bool flag = true;
                    TimeSlot timeSlot = allAvailableTimeSlots[i];

                    for (int j = 1; j < amount; j++)
                    {
                        if ( allAvailableTimeSlots[i+j].Number != timeSlot.Number + j)
                        {
                            flag = false;
                            break;
                        }
                    }

                    if (flag)
                    {
                        timeSlots.Add(timeSlot);
                    }
                }
            }
            return timeSlots;

        }
        private List<TimeSlot> GetAllAvailableTimeSlots()
        {
            List<TimeSlot> timeSlots = new List<TimeSlot>();
            timeSlots.AddRange(TimeSlots.Where(ts => ts.IsAvailable));
            return timeSlots;
        }
        private List<TimeSlot> CreateTimeSlots(WorkingHours workingHours)
        {
            List<TimeSlot> timeSlots = new List<TimeSlot>();

            DateTime timeSlotTime = new DateTime();
            timeSlotTime = timeSlotTime.Add( workingHours.StartTime.TimeOfDay );
            TimeSpan timeSlotDuration = new TimeSpan(0, 0, timeSlotDurationMinute, 0).Duration();

            TimeSlot timeSlot = new TimeSlot(1, timeSlotTime, timeSlotDuration, true);

            int amountAfTimeSlot = timeSlot.GetSlotsAmount(workingHours);

            if (amountAfTimeSlot > 0)
            {
                timeSlots.Add(timeSlot);
                for (int i = 1; i < amountAfTimeSlot; i++)
                {
                    timeSlotTime = timeSlotTime.Add(timeSlotDuration);
                    if (timeSlotTime >= workingHours.StartLunch && 
                        timeSlotTime < workingHours.StartLunch + workingHours.LunchDuration.Duration())
                    {
                        timeSlots.Add(new TimeSlot(i + 1, timeSlotTime, timeSlotDuration, false));
                    }
                    else
                    {
                        timeSlots.Add(new TimeSlot(i + 1, timeSlotTime, timeSlotDuration, true));
                    }
                    
                }
            }
            return timeSlots;
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

        private bool ValidateDate(DateTime date)
        {
            if (date.Hour == 0 && date.Minute == 0 && date.Second == 0 && date.Millisecond == 0 )
            {
                return true;
            }
            return false;
        }
       
    }
}
