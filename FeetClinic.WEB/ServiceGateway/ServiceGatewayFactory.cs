﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BE.BE;
using BE.BE.Customer;
using BE.BE.Schedule;
using BE.BE.Treatments;
using FeetClinic.WEB.Controllers;

namespace FeetClinic.WEB.ServiceGateway
{
    public class ServiceGatewayFactory
    {
        private ServiceGateway<CustomerProfile> _customers;
        private ServiceGateway<TreatmentType> _treatmentTypes;
        private ServiceGateway<Treatment> _treatments;
        private ServiceGateway<Address> _addresses;
        private ServiceGateway<Therapist> _therapists;
        private TimeSlotServiceGateway _timeslots;
        private BookingServiceGateway _bookings;

        public BookingServiceGateway BookingGateway =>
            _bookings ?? (_bookings = new BookingServiceGateway("api/Bookings/"));
        public ServiceGateway<CustomerProfile> CustomersGateway =>
            _customers ?? (_customers = new ServiceGateway<CustomerProfile>("api/Customers/"));

        public ServiceGateway<TreatmentType> TreatmentTypeGateway => 
            _treatmentTypes ?? (_treatmentTypes = new ServiceGateway<TreatmentType>("api/TreatmentTypes/"));

        public ServiceGateway<Treatment> TreatmentGateway =>
            _treatments ?? (_treatments = new ServiceGateway<Treatment>("api/Treatments/"));

        public ServiceGateway<Address> AddresGateway =>
            _addresses ?? (_addresses = new ServiceGateway<Address>("api/Addresses/"));

        public ServiceGateway<Therapist> TherapistGateway =>
            _therapists ?? (_therapists = new ServiceGateway<Therapist>("api/Therapists/"));

        public TimeSlotServiceGateway TimeSlotGateway=>
            _timeslots ?? (_timeslots = new TimeSlotServiceGateway("api/TimeSlots/"));



    }
}