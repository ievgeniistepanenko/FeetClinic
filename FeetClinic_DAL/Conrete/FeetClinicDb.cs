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
    public  class FeetClinicDb : DbContext
    {
        public FeetClinicDb() : base("FeetClinicDB")
        {
            Configuration.ProxyCreationEnabled = false;
            Database.SetInitializer(new DbInitializer());
        }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<CustomerProfile> CustomerProfiles { get; set; } 
        //public DbSet<Treatment> Treatments { get; set; } 
    }
}
