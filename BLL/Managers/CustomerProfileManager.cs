using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE.BE.Customer;
using FeetClinic_DAL.Abstarct;

namespace BLL.Managers
{
    public class CustomerProfileManager : AbstractManager<CustomerProfile>
    {
        public CustomerProfileManager()
        {
            Repository = Facade.CustomerProfiles;
        }

        protected override IRepository<CustomerProfile> GetRepository()
        {
            return Repository;
        }
    }
}
