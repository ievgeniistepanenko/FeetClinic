using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Configuration;
using BE.BE;
using BE.Interfaces;

namespace FeetClinic.WEB.ServiceGateway
{
    public class TimeSlotServiceGateway
    {
        private readonly string path;
        private string hostUri;

        public TimeSlotServiceGateway(string path, string hostUri = "")
        {
            this.hostUri = hostUri;
            this.path = path;
        }

        public IEnumerable<IEnumerable<ITimeSlot>> GetFreeTimeSlotsForTherapist(int therapistId, int week, int year)
        {
            IEnumerable<IEnumerable<ITimeSlot>> timeSlots = new List<List<ITimeSlot>>();
            HttpClient client = GetHttpClient();
            HttpResponseMessage response = client
                .GetAsync(path + "?therapistId=" + therapistId + "&week="+week+"&year="+year).Result;

            if (response.StatusCode == HttpStatusCode.OK)
            {
                timeSlots = response.Content.ReadAsAsync<IEnumerable<IEnumerable<ITimeSlot>>>().Result;
            }
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return timeSlots;
            }
            if (response.StatusCode == HttpStatusCode.InternalServerError)
            {
                throw new Exception(response.ReasonPhrase);
            }
            return timeSlots;
        } 


        private HttpClient GetHttpClient()
        {
            string baseAddress =
                WebConfigurationManager.AppSettings["FeetClinicRestApiBaseAddress"];
            if (hostUri == "")
            {
                hostUri = baseAddress;
            }
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(hostUri);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return client;
        }

    }
}