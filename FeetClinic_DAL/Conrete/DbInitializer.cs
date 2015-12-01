using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel.BE;

namespace FeetClinic_DAL.Conrete
{
    public class DbInitializer : DropCreateDatabaseAlways<FeetClinicDb>
    {
        protected override void Seed(FeetClinicDb context)
        {
            Address address = new Address {City = "Esbjerg", Id = 1,StreetName = "Stormgade",StreetNumber = "36",ZipCode = 7660};
            context.Addresses.Add(address);
            context.SaveChanges();
            //context.Orders.Add(order); 
            base.Seed(context);
        }

    }
}
