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
using FeetClinic_DAL.Abstarct;

namespace FeetClinic_DAL.Conrete
{
    public class DalFacade : RepositoryContainer<FeetClinicDb>
    {
        public DalFacade()
        {
            Context = new FeetClinicDb();
        }

        
        private Repository<FeetClinicDb, CustomerProfile> _customerProfiles;
        private Repository<FeetClinicDb, Booking> _bookings;
        private Repository<FeetClinicDb, Therapist> _therapists;
        private Repository<FeetClinicDb, Treatment> _treatments;
        private Repository<FeetClinicDb, TreatmentType> _treatmentTypes;
        private Repository<FeetClinicDb, DayWorkingHours> _workingHours;
        private Repository<FeetClinicDb, Holiday> _holidays;
        private Repository<FeetClinicDb, Address> _addresses;

        public Repository<FeetClinicDb, Holiday> Holidays =>
            _holidays ?? (_holidays = new Repository<FeetClinicDb, Holiday>(Context)); 
        public Repository<FeetClinicDb, DayWorkingHours> WorkingHours =>
            _workingHours ?? (_workingHours = new Repository<FeetClinicDb, DayWorkingHours>(Context)); 
        public Repository<FeetClinicDb, Therapist> Therapist
            => _therapists ?? (_therapists = new Repository<FeetClinicDb, Therapist>(Context));

        public Repository<FeetClinicDb, TreatmentType> TreamentTypes
            => _treatmentTypes ?? (_treatmentTypes = new Repository<FeetClinicDb, TreatmentType>(Context)); 


        public Repository<FeetClinicDb,Address> Addresses => 
            _addresses ?? (_addresses = new Repository<FeetClinicDb, Address>(Context));
        public Repository<FeetClinicDb, CustomerProfile> CustomerProfiles =>
            _customerProfiles ?? (_customerProfiles = new Repository<FeetClinicDb, CustomerProfile>(Context));

        public Repository<FeetClinicDb, Booking> Bookings =>
            _bookings ?? (_bookings = new Repository<FeetClinicDb, Booking>(Context)); 
        public Repository<FeetClinicDb, Treatment> Treatments =>
            _treatments ?? (_treatments = new Repository<FeetClinicDb, Treatment>(Context));
    }
}
