using System.Collections.Generic;

namespace DomainModel.BE.Customer
{
    public class CustomerProfile
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public Address Address { get; set; }
        public List<Booking> Bookings { get; set; } 
   

    }

}