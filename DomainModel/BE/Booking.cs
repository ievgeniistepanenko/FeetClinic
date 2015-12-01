using System;
using System.Collections.Generic;
using DomainModel.BE.Customer;

namespace DomainModel.BE
{
   public class Booking
    {
       public int Id { get; set; }

       public DateTime BookingDate { get; set; }

       public DateTime DateTime { get; set; }

       public CustomerProfile CustomerProfile { get; set; }

       public List<Treatment> Treatments { get; set; }

       public Therapist Therapist { get; set; }

       public Booking()
       {
            Treatments = new List<Treatment>();
       }

       public TimeSpan GetDuration()
       {
           TimeSpan time = new TimeSpan(0,0,0,0);
           foreach (Treatment treatment in Treatments)
           {
               time = time.Add(treatment.Duration.Duration());
           }
           return time;
       }

       public override bool Equals(object obj)
       {
           Booking booking = obj as Booking;
           if (booking == null)
           {
               return false;
           }
           return booking.Id == Id;
       }

       public bool Equals(Booking booking)
       {
           if (booking==null)
           {
               return false;
           }
           return booking.Id == Id;
       }
    

    }
}
