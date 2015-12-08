using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BE.Interfaces;
using BLL.Managers;

namespace FeetClinic_Rest.Controllers
{
    public class TimeSlotsController : ApiController
    {
        TimeSlotManager tsManager;

        public TimeSlotsController()
        {
            tsManager = new TimeSlotManager();
        }

        /// <summary>
        /// Return all available timeslot for therapist with given id for week in the year
        /// </summary>
        /// <param name="therapistId">Therapist Id</param>
        /// <param name="week"> Number of week for 1 to 52</param>
        /// <param name="year"> Number of year</param>
        /// <returns>Http response message containing the List of List of ITimeSlot  and a HTTP status code</returns>
        //Get: api/Bookings/1/51/2015
        public HttpResponseMessage GetTimeSlots(int therapistId, int week, int year)
        {
            return GetTimeSlots(therapistId, week, year, "");
        }
        /// <summary>
        /// Return all available timeslot for therapist with given id for week in the year
        /// </summary>
        /// <param name="therapistId">Therapist Id</param>
        /// <param name="week"> Number of week for 1 to 52</param>
        /// <param name="year"> Number of year</param>
        /// <param name="properties">String values representing TimeSlot properties</param>
        /// <returns>Http response message containing the List of List of ITimeSlot  and a HTTP status code</returns>
        //Get: api/Bookings/1/51/2015/CustomerProfile
        public HttpResponseMessage GetTimeSlots(int therapistId, int week, int year, string properties)
        {
            try
            {
                List<List<ITimeSlot>> timeSlots = tsManager.GetAvailableTimeSlots(therapistId, week, year);
                if (timeSlots.Any())
                {
                    return Request.CreateResponse(HttpStatusCode.OK, timeSlots);
                }
                return Request.CreateResponse(HttpStatusCode.NotFound, "Not found");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
