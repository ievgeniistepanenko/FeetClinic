using System;
using System.Collections;
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
        public List<ITimeSlot> TimeSlots { get; } 

        public DayAgenda(DateTime date, IWorkingHours wh,List<Booking> bookings = null  )
        {

            Date = date.Date;
            TimeSlots = CreateTimeSlots(wh,timeSlotDurationMinute);

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
            List<ITimeSlot> availableTimeSlots = GetAvailableTimeSlots(booking);
            if (!availableTimeSlots.Any())
            {
                throw new ArgumentException("This is no place for booking");
            }

            int amount = 0;
            ITimeSlot firstTimeSlot = availableTimeSlots
                .FirstOrDefault(ts => ts.StartTime <= booking.DateTime.TimeOfDay
                                && ts.StartTime.Add(ts.Duration)  > booking.DateTime.TimeOfDay);

            if (firstTimeSlot != null)
            {
                amount = firstTimeSlot.GetSlotsAmount(booking.GetDuration());
                for (int i = 0; i < amount; i++)
                {
                    TimeSlots.Find(ts => ts.Number == firstTimeSlot.Number + i).IsAvailable = false;
                }
                Bookings.Add(booking);
                return booking;
            }
            throw new ArgumentException("This is no place for booking");
        }

        public Booking RemoveBooking(Booking booking)
        {
            if (booking.DateTime <= DateTime.Now.AddDays(1))
            {
                throw new OperationCanceledException("Can not remove booking 24 hours before booking time");
            }
            Bookings.Remove(booking);
            ITimeSlot firstTimeSlot = TimeSlots
                .FirstOrDefault(ts=>ts.StartTime == booking.DateTime.TimeOfDay);
            if (firstTimeSlot !=null)
            {
                int amount = firstTimeSlot.GetSlotsAmount(booking.GetDuration());
                for (int i = 0; i < amount; i++)
                {
                    TimeSlots.Find(ts => ts.Number == firstTimeSlot.Number + i).IsAvailable = true;
                }
                return booking;
            }
            

            throw new Exception("Can not delete booking");
        }

        public Booking RemoveBooking(int bookingId)
        {
            Booking booking = Bookings.Find(b => b.Id == bookingId);
            return RemoveBooking(booking);
        }

        public bool IsAvailableForBooking(Booking booking)
        {
            return GetAvailableTimeSlots(booking).Any();
        }

        public List<ITimeSlot> GetAvailableTimeSlots(Booking booking)
        {
            List<ITimeSlot> timeSlots = new List<ITimeSlot>();
            List<ITimeSlot> allAvailableTimeSlots = GetAllAvailableTimeSlots();
            TimeSpan bookingDuration = booking.GetDuration().Duration();

            int amount = 0;
            if (allAvailableTimeSlots.Any())
            {
                amount = allAvailableTimeSlots.First().GetSlotsAmount(bookingDuration);

                for (int i = 0; i <= allAvailableTimeSlots.Count - amount; i++)
                {
                    bool flag = true;
                    ITimeSlot timeSlot = allAvailableTimeSlots[i];

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

        private List<ITimeSlot> GetAllAvailableTimeSlots()
        {
            List<ITimeSlot> timeSlots = new List<ITimeSlot>();
            timeSlots.AddRange(TimeSlots.Where(ts => ts.IsAvailable));
            return timeSlots;
        }
        private List<ITimeSlot> CreateTimeSlots(IWorkingHours workingHours,int slotDurationInMin)
        {
            List<ITimeSlot> timeSlots = new List<ITimeSlot>();
            List<ITimeSlot> whTimeSlots = workingHours.GetWorkingHours();
            whTimeSlots.Sort((ts1, ts2) => ts1.StartTime.CompareTo(ts2.StartTime));

            Time firstSlotTime = whTimeSlots[0].StartTime;
            TimeSpan timeSlotDuration = new TimeSpan(0,slotDurationInMin,0);

            ITimeSlot slot = new TimeSlot(1,firstSlotTime,timeSlotDuration,true);  // first timeslot
            int amountOfTimeSlots = slot.GetSlotsAmount(workingHours.GetWorkDayDuration());
            if (amountOfTimeSlots > 0)
            {
                timeSlots.Add(slot);
                for (int i = 1; i < amountOfTimeSlots; i++)
                {
                    Time nextSlotTime = slot.StartTime.Add(slot.Duration);
                    if (whTimeSlots.Any(ts=> nextSlotTime >= ts.StartTime 
                                        && nextSlotTime < ts.StartTime.Add(ts.Duration)))
                    {
                        slot = new TimeSlot(i + 1, slot.StartTime.Add(slot.Duration), timeSlotDuration, true);
                    }
                    else
                    {
                        slot = new TimeSlot(i + 1, slot.StartTime.Add(slot.Duration), timeSlotDuration, false);
                    }
                    timeSlots.Add(slot);
                    
                }
            }
            return timeSlots;
        }
        //sets timeslots unavailable if some booking is cover their
        private void UpdateTimeSlots(List<Booking> bookings)
        {
            foreach (Booking booking in bookings)
            {
                ITimeSlot firstTimeSlot = TimeSlots.Find(ts =>
                ts.StartTime <= booking.DateTime.TimeOfDay &&
                ts.StartTime.Add(ts.Duration)  > booking.DateTime.TimeOfDay);
                firstTimeSlot.IsAvailable = false;

                int amounTimeSlots = firstTimeSlot.GetSlotsAmount( booking.GetDuration() );

                for (int i = 1; i < amounTimeSlots; i++)
                {
                    ITimeSlot timeSlot = TimeSlots.Find(ts => ts.Number == firstTimeSlot.Number + i);
                    if (timeSlot.IsAvailable == false)
                    {
                        throw new ArgumentException("Given bookings kan not apply for this day agenda");
                    }
                    timeSlot.IsAvailable = false;
                }
            }
        }
       
    }
}
