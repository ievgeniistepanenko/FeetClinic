using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel.BE;
using DomainModel.BE.Schedule;

namespace DomainModel.Interfaces
{
    public interface IDayAgenda
    {
        DateTime Date { get;}
        List<Booking> Bookings { get;}
        Booking AddBooking(Booking booking);
        Booking RemoveBooking(Booking booking);
        Booking RemoveBooking(int bookingId);
        bool IsAvailableForBooking(Booking booking);
        List<TimeSlot> GetAvailableTimeSlots(Booking booking);
    }
}
