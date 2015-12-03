using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel.BE;
using DomainModel.BE.Customer;

namespace FeetClinic_DAL.Conrete
{
    public class DbInitializer : DropCreateDatabaseAlways<FeetClinicDb>
    {
        protected override void Seed(FeetClinicDb context)
        {
            //List<Booking> bookings = new List<Booking>
            //{
            //    new Booking {DateTime  = DateTime.Now.AddDays(2), BookingDate = DateTime.Now},
            //    new Booking {DateTime  = DateTime.Now.AddDays(2), BookingDate = DateTime.Now}
            //};
            Address address = new Address {City = "Esbjerg", Id = 1,StreetName = "Stormgade",StreetNumber = "36",ZipCode = 7660};
            CustomerProfile customer = new CustomerProfile
            { Address = address, /*Bookings = bookings,*/FirstName = "Lars",Id = 1, LastName = "Bilde" };

            //context.Addresses.Add(address);
            context.CustomerProfiles.Add(customer);
            context.SaveChanges();
            base.Seed(context);
        }

    }
}
