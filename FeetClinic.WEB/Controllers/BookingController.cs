using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BE.BE;
using BE.BE.Schedule;
using BE.BE.Treatments;
using BE.Interfaces;
using BE.Services;
using FeetClinic.WEB.Models;
using FeetClinic.WEB.ServiceGateway;

namespace FeetClinic.WEB.Controllers
{
    [Authorize]
    public class BookingController : Controller
    {
        private ServiceGatewayFactory factory;

        public BookingController()
        {
            factory = new ServiceGatewayFactory();
        }

        [Authorize]
        // GET: Booking
        public ActionResult Index(int userId)
        {
            IEnumerable<Booking> bookings = factory.BookingGateway.GetForCustomer(userId);
            return View(bookings);
        }

        // GET: Booking/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Booking/Create
        public ActionResult Create(int? therapistId,DateTime? date)
        {

            CreateBookingViewModel model = new CreateBookingViewModel();

            model.TherapistsSelectListItems = GetTherapists();
            model.TreatmentsSelectListItems = therapistId == null ? 
                GetTreatments() : GetTreatments(therapistId.Value);
            if (date == null)
            {
                date = DateTime.Today;
                model.DateTime = date.Value;
            }

            model.WeekFreeTimes = new List<DayTimeSlotViewModel>();
            if (therapistId != null)
            {
                int week = CalendarService.GetWeekOfYear(date.Value);
                int year = date.Value.Year;
                DateTime firsDay = CalendarService.FirstDateOfWeekISO8601(year, week);
                IEnumerable<IEnumerable<ITimeSlot>> freeTimeSlotsForTherapist = factory.TimeSlotGateway.GetFreeTimeSlotsForTherapist(
                    therapistId.Value, week, year);
                for (int i = 0; i < 7; i++)
                {
                    DayTimeSlotViewModel dayTimeSlotViewModel = new DayTimeSlotViewModel();
                    dayTimeSlotViewModel.TimeSlots = new List<ITimeSlot>();
                    dayTimeSlotViewModel.Date = firsDay.AddDays(i);
                    dayTimeSlotViewModel.TimeSlots = freeTimeSlotsForTherapist.ElementAt(i).ToList();
                    model.WeekFreeTimes.Add(dayTimeSlotViewModel);
                }
            }
            else
            {
                for (int i = 0; i < 7; i++)
                {
                    DayTimeSlotViewModel dayTimeSlotViewModel = new DayTimeSlotViewModel();
                    dayTimeSlotViewModel.TimeSlots = new List<ITimeSlot>();
                    model.WeekFreeTimes.Add(dayTimeSlotViewModel);
                }
            }
            
                return View(model);
        
        }

        // POST: Booking/Create
        [HttpPost]
        public ActionResult Create(CreateBookingViewModel model)
        {
            try
            {
                Booking booking = new Booking();

                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

       

        // GET: Booking/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Booking/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        private SelectList GetTherapists()
        {
            var allTheras = factory.TherapistGateway.GetAll()
                        .Select(x =>
                                new SelectListItem()
                                {
                                    Value = x.Id.ToString(),
                                    Text = x.Name
                                });

            return new SelectList(allTheras, "Value", "Text");
        }

        private SelectList  GetTreatments()
        {
            var allTreatments = factory.TreatmentGateway.GetAll()
                .Select(t => new SelectListItem()
                {
                    Value = t.Id.ToString(),
                    Text = t.Name
                });

            return new SelectList(allTreatments,"Value","Text");
        }
        private SelectList GetTreatments(int therapistId)
        {
            var therapist = factory.TherapistGateway.GetOne(therapistId, "Treatments");
            var allTreatments = therapist.Treatments
                .Select(t => new SelectListItem()
                {
                    Value = t.Id.ToString(),
                    Text = t.Name
                });

            return new SelectList(allTreatments, "Value", "Text");
        }

        
    }
}
