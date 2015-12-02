using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BLL.Managers;
using DomainModel.BE.Customer;

namespace FeetClinic_Rest.Controllers
{
    public class CustomerController : AbstractApiController<CustomerProfile,CustomerProfileManager>
    {
        public CustomerController()
        {
            Manager = BllFacade.CustomerManager;
        }
        protected override CustomerProfileManager GetManager()
        {
            return Manager;
        }
    }
}
