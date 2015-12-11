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
            if (response.StatusCode == HttpStatusCode.OK)
            {
                entity = response.Content.ReadAsAsync<IEnumerable<Booking>>().Result;
            }
            else
            {
                entity = new List<Booking>();
            }
            
            return entity;
        }

        public IEnumerable<Booking> GetForCustomer(int customerId, string properties)
        {
            HttpClient client = GetHttpClient();
            HttpResponseMessage response = client.GetAsync(path + "?customerId=" + customerId + "&properties=" + properties).Result;
            var entity = response.Content.ReadAsAsync<IEnumerable<Booking>>().Result;
            return entity;
        }

        public IEnumerable<Booking> GetForTherapist(int therapistId, int week, int year)
        {
            HttpClient client = GetHttpClient();
            HttpResponseMessage response = client.GetAsync(path + "?therapistId=" + therapistId +
                "&week=" + week + "&year=" + year).Result;
            var entity = response.Content.ReadAsAsync<IEnumerable<Booking>>().Result;
            return entity;
        }

        public IEnumerable<Booking> GetForTherapist(int therapistId, int week, int year, string properties)
        {
            HttpClient client = GetHttpClient();
            HttpResponseMessage response = client.GetAsync(path + "?therapistId=" + therapistId +
                "&week=" + week + "&year=" + year + "&properties=" + properties).Result;
            var entity = response.Content.ReadAsAsync<IEnumerable<Booking>>().Result;
            return entity;
        }
    }
}