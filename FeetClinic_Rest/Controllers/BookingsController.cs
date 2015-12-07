using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BE.BE;
using BLL.Managers;
using DomainModel.BE;

namespace FeetClinic_Rest.Controllers
{
    public class BookingsController : AbstractApiController<Booking,BookingsManager>
    {
        public BookingsController()
        {
            Manager = BllFacade.BookingsManager;
        }

        protected override BookingsManager GetManager()
        {
            return Manager;
        }
        public HttpResponseMessage GetBookings()
        {
            return GetAll();
        }

        // GET: api/Bookings/5
        public HttpResponseMessage GetBooking(int id)
        {
            return GetBooking(id, "");
        }
        public HttpResponseMessage GetBooking(int id, string properties)
        {
            return GetOne(id, properties);
        }

        // PUT: api/Bookings/5
        public HttpResponseMessage PutBookings(int id, Booking booking)
        {
            return UpdateOne(id, booking);
        }

        // POST: api/Bookings
        public HttpResponseMessage PostBooking(Booking booking)
        {
            return CreateOne(booking);
        }

        // DELETE: api/Bookings/5
        public HttpResponseMessage DeleteBooking(int id)
        {
            return DeleteOne(id);
        }
    }
}
