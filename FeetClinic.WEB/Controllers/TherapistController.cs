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
            Therapist therapist = service.TherapistGateway.GetOne(id, "Treatments,WorkingHourses");

            if (therapist == null)
            {
                return HttpNotFound();
            }
            return View(therapist);
        }
        [Authorize(Roles = "admin")]
        // GET: Therapist/Create
        public ActionResult Create()
        {
            var model = new TherapistViewModel
            {
                treats = GetTreatment(),
            };
            return View(model);
        }

        // POST: Therapist/Create
        [HttpPost]
        public ActionResult Create(TherapistViewModel model)
        {

                if (ModelState.IsValid)
                {
                    
                Treatment treatment = service.TreatmentGateway.GetOne(model.SelectedTreatmentId);

                List<Treatment> treats = new List<Treatment>();

                treats.Add(treatment);
                Therapist therapist = new Therapist
                {
                    Name = model.Name,
                    Description = model.Description,

                    Treatments = treats
                };

                service.TherapistGateway.CreateOne(therapist);
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

        private IEnumerable<SelectListItem> GetTreatment()
        {
            var treats = service.TreatmentGateway.GetAll()
                        .Select(x =>
                                new SelectListItem
                                {
                                    Value = x.Id.ToString(),
                                    Text = x.Name
                                });

            return new SelectList(treats, "Value", "Text");
        }


    }
}
