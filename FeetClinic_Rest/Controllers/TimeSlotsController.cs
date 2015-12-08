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

        public HttpResponseMessage GetTimeSlots(int therapistId, int week, int year)
        {
            return GetTimeSlots(therapistId, week, year, "");
        }
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
