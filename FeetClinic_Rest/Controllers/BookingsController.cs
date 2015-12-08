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

        /// <summary>
        /// Return all booking for customer with given id no older than one year
        /// </summary>
        /// <param name="customerId">Customer Id</param>
        /// <returns>Http response message containing the Bookings and a HTTP status code</returns>
        //Get: api/bookings/1
        public HttpResponseMessage GetBookings(int customerId)
        {
            return GetBookings(customerId, "");
        }

        /// <summary>
        /// Return all booking for customer with given id no older than one year
        /// </summary>
        /// <param name="customerId">Customer Id</param>
        /// <param name="properties">String values representing bookings properties</param>
        /// <returns>Http response message containing the Bookings and a HTTP status code</returns>
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

        /// <summary>
        /// Return all booking for therapist with given id for week in the year
        /// </summary>
        /// <param name="therapistId">Therapist Id</param>
        /// <param name="week"> Number of week for 1 to 52</param>
        /// <param name="year"> Number of year</param>
        /// <returns>Http response message containing the List of Booking and a HTTP status code</returns>

        //Get: api/Bookings/1/51/2015
        public HttpResponseMessage GetBookings(int therapistId, int week, int year)
        {
            return GetBookings(therapistId, week, year,"");
        }

        /// <summary>
        /// Return all booking with properties for therapist with given id for week in the year
        /// </summary>
        /// <param name="therapistId">Therapist Id</param>
        /// <param name="week"> Number of week for 1 to 52</param>
        /// <param name="year"> Number of year</param>
        /// <param name="properties">String values representing bookings properties</param>
        /// <returns>Http response message containing the List of Bookings and a HTTP status code</returns>
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

        /// <summary>
        /// Creates a new bookig
        /// </summary>
        /// <param name="booking">Booking</param>
        /// <returns>Http response message containing the booking, its URI, and a HTTP status code</returns>

        // POST: api/Bookings
        public HttpResponseMessage PostBooking(Booking booking)
        {
            return CreateOne(booking);
        }

        
        /// <summary>
        /// Deletes a booking
        /// </summary>
        /// <param name="id">Booking Id of the booking that sould be deleted</param>
        /// <returns>Http response message containing the booking and HTTP status code</returns>
        // DELETE: api/Bookings/5
        public HttpResponseMessage DeleteBooking(int id)
        {
            return DeleteOne(id);
        }
    }
}
