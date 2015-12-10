using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BE.BE.Customer;

namespace FeetClinic.WEB.ServiceGateway
{
    public class ServiceGatewayFactory
    {
        private ServiceGateway<CustomerProfile> _customers;

        public ServiceGateway<CustomerProfile> CustomersGateway =>
            _customers ?? (_customers = new ServiceGateway<CustomerProfile>("api/Customers/"));
    }
}