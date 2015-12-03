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
    public class CustomersController : AbstractApiController<CustomerProfile,CustomerProfileManager>
    {
        public CustomersController()
        {
            Manager = BllFacade.CustomerManager;
        }
        protected override CustomerProfileManager GetManager()
        {
            return Manager;
        }
        public HttpResponseMessage GetCustomers()
        {
            return GetAll();
        }

        // GET: api/Customers/5
        public HttpResponseMessage GetCustomer(int id)
        {
            return GetCustomer(id, "");
        }
        public HttpResponseMessage GetCustomer(int id, string properties)
        {
            return GetOne(id, properties);
        }

        // PUT: api/Customers/5
        public HttpResponseMessage PutCustomer(int id, CustomerProfile customer)
        {
            return UpdateOne(id, customer);
        }

        // POST: api/Customers
        public HttpResponseMessage PostCustomer(CustomerProfile customer)
        {
            return CreateOne(customer);
        }

        // DELETE: api/Customers/5
        public HttpResponseMessage DeleteCustomer(int id)
        {
            return DeleteOne(id);
        }
    }
}
