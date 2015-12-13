using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BE.BE;
using BE.BE.Treatments;
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
        public ActionResult Create(int therapistId,int week)
        {

            BookingViewModel model = new BookingViewModel();

            IEnumerable<Therapist> therapists =  factory.TherapistGateway.GetAll();
            IEnumerable<Treatment> treatments = new List<Treatment>();

            ViewBag.Therapists = therapists;
            if (therapistId !=null && week !=null)
            {
                Therapist therapist = factory.TherapistGateway.GetOne(therapistId, "Treatments");
                model.Therapist = therapist;
            }
            
            
            return View(model);
        }

        // POST: Booking/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Booking/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Booking/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

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
    }
}
