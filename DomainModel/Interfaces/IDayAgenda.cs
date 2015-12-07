using System;
using System.Collections.Generic;
using BE.BE;

namespace BE.Interfaces
{
    public interface IDayAgenda
    {
        DateTime Date { get;}
        List<Booking> Bookings { get;}
        Booking AddBooking(Booking booking);
        Booking RemoveBooking(Booking booking);
        Booking RemoveBooking(int bookingId);
        bool IsAvailableForBooking(Booking booking);
        List<ITimeSlot> GetAvailableTimeSlots(Booking booking);
    }
}
