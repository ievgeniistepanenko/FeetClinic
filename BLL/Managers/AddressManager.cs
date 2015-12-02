using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel.BE;
using FeetClinic_DAL.Abstarct;
using FeetClinic_DAL.Conrete;

namespace BLL.Managers
{
    public class AddressManager : AbstractManager<Address>
    {
        public AddressManager()
        {
            Repository = Facade.Addresses;
        }

        protected override IRepository<Address> GetRepository()
        {
            return Repository;
        }

    }
}
