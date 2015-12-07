using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BE.Interfaces;

namespace BE.BE.Treatments

{
    public class Treatment : IEntity
    {
        [Key]
        public int Id { get; set; }
        //[Required]
        public string Name { get; set; }
        //[Required]
        public string Description { get; set; }
        //[Required]
        //[DataType(DataType.Currency)]
        public decimal Price { get; set; }
        //[Required]
        public TimeSpan Duration { get; set; }
        //[Required]
        public virtual TreatmentType TreatmentType { get; set; }
        //[Required]
        public virtual List<Therapist> Therapists { get; set; }
        public virtual  List<Booking> Bookings { get; set; } 

    }
}
