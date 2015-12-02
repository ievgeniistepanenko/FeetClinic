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
    public class CustomerManager : AbstractManager<Customer>
    {
        protected override FeetClinic_DAL.Abstarct.IRepository<Customer> GetRepository()
        {
            throw new NotImplementedException();
        }
    }
}
