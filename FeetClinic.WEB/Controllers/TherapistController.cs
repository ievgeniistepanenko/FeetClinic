using BE.BE;
using BE.BE.Treatments;
using FeetClinic.WEB.Models;
using FeetClinic.WEB.ServiceGateway;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FeetClinic.WEB.Controllers
{
    public class TherapistController : Controller
    {
        private readonly ServiceGatewayFactory service;


        public TherapistController()
        {
            service = new ServiceGatewayFactory();
        }

        // GET: Therapist
        public ActionResult Index()
        {
            IEnumerable<Therapist> therapists = service.TherapistGateway.GetAll();
            return View(therapists);
        }

        // GET: Therapist/Details/5
        public ActionResult Details(int id)
        {
            Therapist therapist = service.TherapistGateway.GetOne(id);

            if (therapist == null)
            {
                return HttpNotFound();
            }
            return View(therapist);
        }

        // GET: Therapist/Create
        public ActionResult Create()
        {
            IEnumerable<Treatment> treats = service.TreatmentGateway.GetAll();
            return View();
        }

        // POST: Therapist/Create
        [HttpPost]
        public ActionResult Create(TherapistViewModel model)
        {

                if (ModelState.IsValid)
                {
                        

                Therapist therapist = new Therapist
                {
                    Name = model.Name,
                    Description = model.Description,
                };


                    return RedirectToAction("Index");
                }
                return View();

        }

        // GET: Therapist/Edit/5
        public ActionResult Edit(int id)
        {
            Therapist therapist = service.TherapistGateway.GetOne(id);
            if (therapist == null)
            {
                return HttpNotFound();
            }
            return View(therapist);
        }

        // POST: Therapist/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Therapist therapist)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    service.TherapistGateway.Update(therapist);

                    return RedirectToAction("Index");
                }
                return View(therapist);
            }
            catch
            {
                return View();
            }
        }

        // GET: Therapist/Delete/5
        public ActionResult Delete(int id)
        {
            Therapist therapist = service.TherapistGateway.GetOne(id);
            return View(therapist);
        }

        // POST: Therapist/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Therapist therapist)
        {
            try
            {
                // TODO: Add delete logic here
                service.TherapistGateway.Delete(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


    }
}
