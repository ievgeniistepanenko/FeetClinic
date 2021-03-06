﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BE.BE.Customer;
using BE.BE.Treatments;
using BE.Interfaces;

namespace BE.BE
{
   public class Booking : IEntity
    {
       [Key]
       public int Id { get; set; }
        [Required]
        public DateTime BookingDate { get; set; }
        [Required]
        public DateTime DateTime { get; set; }
        public CustomerProfile CustomerProfile { get; set; }
        [Required]
        public int CustomerProfileId { get; set; }
        [Required]
        public List<Treatment> Treatments { get; set; }
        public Therapist Therapist { get; set; }
        [Required]
        public int TherapistId { get; set; }

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

       public decimal GetPrice()
       {
           decimal price = 0;
            foreach (Treatment treatment in Treatments)
            {
                price = price + treatment.Price;
            }
           return price;
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
