using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BE.BE.Schedule;
using FeetClinic.WEB.Models;

namespace FeetClinic.WEB.Infrastructure
{
    public class CreateTherapistBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            CreateTherapistViewModel model = (CreateTherapistViewModel) bindingContext.Model
                                             ?? new CreateTherapistViewModel();


            model.WorkingHourses = new List<DayWorkingHours>();

            HttpRequestBase request = controllerContext.HttpContext.Request;

            for (int i = 0; i < 7; i++)
            {
                string start = request.Form.Get("WorkingHourses.[" + i + "].StartTime");
                string end = request.Form.Get("WorkingHourses.[" + i + "].EndTime");
                string startLunch = request.Form.Get("WorkingHourses.[" + i + "].StartLunch");
                string duration = request.Form.Get("WorkingHourses.[" + i + "].LunchDuration");

                Time startTime = new Time(GetHours(start),GetMinutes(start));
                Time endTime = new Time(GetHours(end), GetMinutes(end));
                Time startLunchTime = new Time(GetHours(startLunch), GetMinutes(startLunch));
                TimeSpan durationTime = new TimeSpan(GetHours(duration),GetMinutes(duration),GetSeconds(duration));

                try
                {
                    DayWorkingHours wh = new DayWorkingHours(startTime, endTime, startLunchTime, durationTime);
                    wh.DayOfWeek = (i + 1) % 7;
                    model.WorkingHourses.Add(wh);
                }
                catch (Exception)
                {
                   // Ignore
                }
                
            }
            model.Description = GetValue(request, "Description");
            model.Name =  GetValue(request, "Name");


            string [] treatmentsId =  request.Form.GetValues("SelectedTreatmentId");
            if (treatmentsId != null) model.SelectedTreatmentId = 
                    Array.ConvertAll(treatmentsId , int.Parse);

            return model;
        }

        private int GetHours(string time)
        {
           string[] arr =  time.Split(new[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
            try
            {
                return Int32.Parse(arr[0]);
            }
            catch (Exception)
            {
                
                return 0;
            }
            
        }

        private int GetMinutes(string time)
        {
            string[] arr = time.Split(new[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
            try
            {
                return Int32.Parse(arr[1]);
            }
            catch (Exception)
            {
                
                return 0;
            }
            
        }
        private int GetSeconds(string time)
        {
            string[] arr = time.Split(new[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
            try
            {
                return Int32.Parse(arr[2]);
            }
            catch (Exception)
            {

                return 0;
            }
            
        }

        private string GetValue(HttpRequestBase request, string name)
        {
            return request.Form.Get(name);
        }
    }
}