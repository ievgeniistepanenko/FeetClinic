 using System;
using System.Collections.Generic;
using System.Linq;
 using System.Net;
 using System.Net.Http;
 using System.Web;
 using BE.BE;

namespace FeetClinic.WEB.ServiceGateway
{
    public class BookingServiceGateway : ServiceGateway<Booking>
    {
        public BookingServiceGateway(string path, string hostUri = "") : base(path, hostUri)
        {
        }

        public IEnumerable<Booking> GetForCustomer(int customerId)
        {
            IEnumerable<Booking> entity;
            HttpClient client = GetHttpClient();
            HttpResponseMessage response = client.GetAsync(path + "?customerId="+customerId).Result;
            return GetBookingsFromResponse(response);
        }

        public IEnumerable<Booking> GetForCustomer(int customerId, string properties)
        {
            HttpClient client = GetHttpClient();
            HttpResponseMessage response = client.GetAsync(path + "?customerId=" + customerId + "&properties=" + properties).Result;
            return GetBookingsFromResponse(response);
        }

        public IEnumerable<Booking> GetForTherapist(int therapistId, int week, int year)
        {
            HttpClient client = GetHttpClient();
            HttpResponseMessage response = client.GetAsync(path + "?therapistId=" + therapistId +
                "&week=" + week + "&year=" + year).Result;
            return GetBookingsFromResponse(response);
        }

        public IEnumerable<Booking> GetForTherapist(int therapistId, int week, int year, string properties)
        {
            HttpClient client = GetHttpClient();
            HttpResponseMessage response = client.GetAsync(path + "?therapistId=" + therapistId +
                "&week=" + week + "&year=" + year + "&properties=" + properties).Result;
            return GetBookingsFromResponse(response);
        }

        private IEnumerable<Booking> GetBookingsFromResponse(HttpResponseMessage response)
        {
            IEnumerable<Booking> bookings = new List<Booking>();
            if (response.StatusCode == HttpStatusCode.OK)
            {
                bookings = response.Content.ReadAsAsync<IEnumerable<Booking>>().Result;
            }
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return bookings;
            }
            if (response.StatusCode == HttpStatusCode.InternalServerError)
            {
                throw new Exception(response.ReasonPhrase);
            }
            return bookings;
        } 
    }
}