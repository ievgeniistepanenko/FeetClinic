using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE.BE;
using BE.BE.Customer;
using BE.BE.Schedule;
using BE.BE.Treatments;

namespace FeetClinic_DAL.Conrete
{
    public  class FeetClinicDb : DbContext
    {
        public FeetClinicDb() : base("FeetClinicDB")
        {
            Configuration.ProxyCreationEnabled = false;
            //Database.SetInitializer(new DbInitializer());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
        public DbSet<Booking> Bookings { get; set; }
        
    }
}
