using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel.BE;
using DomainModel.BE.Customer;
using DomainModel.BE.Schedule;
using FeetClinic_DAL.Abstarct;

namespace FeetClinic_DAL.Conrete
{
    public class DalFacade : RepositoryContainer<Database>
    {
        public DalFacade()
        {
            Context = new Database();
        }

        public Repository<Database, Address> Addresses;
        public Repository<Database, CustomerProfile> CustomerProfiles;
        public Repository<Database, Booking> Bookings;
        public Repository<Database, Therapist> Therapists;
        public Repository<Database, Treatment> Treatments;
        public Repository<Database, TreatmentType> TreatmentTypes;
        //public Repository<Database, DayAgenda> agendas;
        //public Repository<Database, YearsScheduler> yearsScheduler;
        //public Repository<Database, DayWorkingHours> workingHours;

    }
}
