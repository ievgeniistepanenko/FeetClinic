using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE.BE;
using BE.BE.Customer;
using BE.BE.Schedule;
using BE.BE.Treatments;

namespace FeetClinic_DAL.Conrete
{
    public class DbInitializer : DropCreateDatabaseAlways<FeetClinicDb>
    {
        protected override void Seed(FeetClinicDb context)
        {
            
            Address address = new Address {City = "Esbjerg", Id = 1,StreetName = "Stormgade",StreetNumber = "36",ZipCode = 7660};
            CustomerProfile customer = new CustomerProfile
            { Address = address, FirstName = "Lars",Id = 1, LastName = "Larsen" };

            List<DayWorkingHours> listWh = new List<DayWorkingHours>();
            for (int i = 1; i < 6; i++)
            {
                DayWorkingHours working = new DayWorkingHours(new Time(8, 0), new Time(15, 0), new Time(11, 30), new TimeSpan(0, 0, 30, 0));
                working.DayOfWeek = i;
                listWh.Add(working);
            }

            Therapist therapist = new Therapist
            {
                Id = 3,
                Description = "Pisse træls ",
                Name = "andy",
                WorkingHourses = listWh
                
            };

            TreatmentType treatment = new TreatmentType
            {
                Id = 1,
                Name = "behandling",
               
            };

            Treatment treatment1 = new Treatment
            {
                Description = "hår ",
                Name = "first",
                Duration = new TimeSpan(0, 45, 0),
                Price = 300,
                TreatmentType = treatment,
                Therapists = new List<Therapist> { therapist}
            };


            Booking booking = new Booking
            {
                DateTime = DateTime.Now.AddDays(1).AddHours(2),
                BookingDate = DateTime.Now,
                CustomerProfile = customer,
                Therapist = therapist,
                Treatments = new List<Treatment> {treatment1}
            };


            context.Bookings.Add(booking);
            context.SaveChanges();
            base.Seed(context);
        }

    }
}
