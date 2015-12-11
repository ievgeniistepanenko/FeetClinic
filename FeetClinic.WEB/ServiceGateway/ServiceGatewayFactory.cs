using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BE.BE;
using BE.BE.Customer;
using BE.BE.Treatments;
using FeetClinic.WEB.Controllers;

namespace FeetClinic.WEB.ServiceGateway
{
    public class ServiceGatewayFactory
    {
        private ServiceGateway<CustomerProfile> _customers;
        private ServiceGateway<TreatmentType> _treatmentTypes;
        private ServiceGateway<Treatment> _treatment;
        private ServiceGateway<Booking> _bookings; 


             
        public ServiceGateway<CustomerProfile> CustomersGateway =>
            _customers ?? (_customers = new ServiceGateway<CustomerProfile>("api/Customers/"));

        public ServiceGateway<TreatmentType> TreatmentTypeGateway => 
            _treatmentTypes ?? (_treatmentTypes = new ServiceGateway<TreatmentType>("api/TreatmentTypes/"));

        public ServiceGateway<Treatment> TreatmentGateway =>
            _treatment ?? (_treatment = new ServiceGateway<Treatment>("api/Treatment/"));
    }
}