using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeetClinic_DAL.Conrete
{
    public  class Database : DbContext
    {
        public Database() : base("FeetClinicDB")
        {
            Configuration.ProxyCreationEnabled = false;
        }
    }
}
