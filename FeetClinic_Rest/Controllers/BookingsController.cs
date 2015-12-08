using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BE.BE;
using BLL.Managers;

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
        //Get: api/bookings/1
        public HttpResponseMessage GetBookings(int customerId)
        {
            return GetBookings(customerId, "");
        }
        //Get: api/bookings/1/Treatments
        public HttpResponseMessage GetBookings(int customerId, string properties)
        {
            try
            {
                List<Booking> bookings = Manager.GetAllForCustomer(customerId,properties).ToList();
                if (bookings.Any())
                {
                    return Request.CreateResponse(HttpStatusCode.OK, bookings);
                }
                return Request.CreateResponse(HttpStatusCode.NotFound, "Not found");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        //Get: api/Bookings/1/51/2015
        public HttpResponseMessage GetBookings(int therapistId, int week, int year)
        {
            return GetBookings(therapistId, week, year,"");
        }
        //Get: api/Bookings/1/51/2015/CustomerProfile
        public HttpResponseMessage GetBookings(int therapistId,int week, int year, string properties)
        {
            try
            {
                List<Booking> bookings = Manager.GetAllForTherapistForWeek(therapistId, week, year, properties).ToList();
                if (bookings.Any())
                {
                    return Request.CreateResponse(HttpStatusCode.OK, bookings);
                }
                return Request.CreateResponse(HttpStatusCode.NotFound, "Not found");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
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
