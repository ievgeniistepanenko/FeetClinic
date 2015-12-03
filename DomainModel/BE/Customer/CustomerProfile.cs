using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DomainModel.BLL.Interfaces;

namespace DomainModel.BE.Customer
{
    public class CustomerProfile : IEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MinLength(2),MaxLength(50)]
        public string FirstName { get; set; }
        [Required]
        [MinLength(2), MaxLength(50)]
        public string LastName { get; set; }
        public virtual Address Address { get; set; }
        public virtual List<Booking> Bookings { get; set; }

        //public CustomerProfile()
        //{
        //    Address = new Address();
        //    Bookings = new List<Booking>();
        //}

    }

}