using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE.BE.Customer;

namespace FeetClinic_DAL.Conrete
{
    public  class FeetClinicDb : DbContext
    {
        public FeetClinicDb() : base("FeetClinicDB")
        {
            Configuration.ProxyCreationEnabled = false;
            Database.SetInitializer(new DbInitializer());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<CustomerProfile> CustomerProfiles { get; set; } 
        //public DbSet<Treatment> Treatments { get; set; } 
    }
}
