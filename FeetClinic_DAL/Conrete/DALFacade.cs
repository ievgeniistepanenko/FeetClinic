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
    public class DalFacade : RepositoryContainer<FeetClinicDb>
    {
        public DalFacade()
        {
            Context = new FeetClinicDb();
        }

        private Repository<FeetClinicDb, Address> _addresses;
        //public Repository<FeetClinicDb, CustomerProfile> CustomerProfiles;
        //public Repository<FeetClinicDb, Booking> Bookings;
        //public Repository<FeetClinicDb, Therapist> Therapists;
        private Repository<FeetClinicDb, Treatment> _treatments;
        //public Repository<FeetClinicDb, TreatmentType> TreatmentTypes;
        //public Repository<FeetClinicDb, DayAgenda> agendas;
        //public Repository<FeetClinicDb, YearsScheduler> yearsScheduler;
        //public Repository<FeetClinicDb, DayWorkingHours> workingHours;
        public Repository<FeetClinicDb,Address> Addresses => 
            _addresses ?? (_addresses = new Repository<FeetClinicDb, Address>(Context));

        public Repository<FeetClinicDb, Treatment> Treatments =>
            _treatments ?? (_treatments = new Repository<FeetClinicDb, Treatment>(Context));
    }
}
