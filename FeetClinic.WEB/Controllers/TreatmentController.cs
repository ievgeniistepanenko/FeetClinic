using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BE.BE.Treatments;
using FeetClinic.WEB.ServiceGateway;
using BE.BE;
using FeetClinic.WEB.Models;

namespace FeetClinic.WEB.Controllers
{
    public class TreatmentController : Controller
    {
        private readonly ServiceGatewayFactory service;

        public TreatmentController()
        {
            service = new ServiceGatewayFactory();
        }

        // GET: Treatment
        public ActionResult Index()
        {
            IEnumerable<Treatment> treat = service.TreatmentGateway.GetAll();
            return View(treat);
        }


        // GET: Treatment/Create
        public ActionResult Create()
        {
            var model = new TreatmentCreateViewModel
            {
                Therap = GetTherapists(),
                Types = GetTypes()
            };
            return View(model);
        }

        // POST: Treatment/Create
        [HttpPost]
        public ActionResult Create(TreatmentCreateViewModel model)
        {

            if (ModelState.IsValid)
            {
                List<Therapist> allThera = new List<Therapist>();
                foreach (int id in model.SelectedTherapistId)
                {
                    Therapist thera = service.TherapistGateway.GetOne(id);
                    allThera.Add(thera);
                }
                Treatment treatment = new Treatment
                {
                    Name = model.Name,
                    Description = model.Description,
                    Price = model.Price,
                    Duration = model.Duration,
                    TreatmentTypeId = model.SelectedTypeId,
                };

                service.TreatmentGateway.CreateOne(treatment);
                treatment.Therapists = allThera;
                service.TreatmentGateway.Update(treatment);

                return RedirectToAction("Index");
            }
            return View();

        }

        // GET: Treatment/Edit/5
        public ActionResult Edit(int id)
        {
            Treatment treat = service.TreatmentGateway.GetOne(id,"TreatmentType,Therapists");
            if (treat == null)
            {
                return HttpNotFound();
            }

            TreatmentEditViewModel model = new TreatmentEditViewModel();
            model.Id = treat.Id;
            model.Duration = treat.Duration;
            model.Description = treat.Description;
            model.Name = treat.Name;
            model.Price = treat.Price;
            model.Therap = GetTherapists();
            model.Types = GetTypes();
            
            return View(model);
        }

        // POST: Treatment/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, TreatmentEditViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    List<Therapist> allThera = new List<Therapist>();
                    foreach (int therapistId in model.SelectedTherapistId)
                    {
                        Therapist thera = service.TherapistGateway.GetOne(therapistId);
                        allThera.Add(thera);
                    }
                    Treatment treatment = new Treatment
                    {
                        Id = id,
                        Name = model.Name,
                        Description = model.Description,
                        Price = model.Price,
                        Duration = model.Duration,
                        TreatmentTypeId = model.SelectedTypeId,
                        Therapists = allThera
                };

                    
                    service.TreatmentGateway.Update(treatment);
                    return RedirectToAction("Index");
                }
                return View(model);
            }
            catch
            {
                return View();
            }
        }


        // GET: Treatment/Delete/5
        public ActionResult Delete(int id)
        {
            Treatment treat = service.TreatmentGateway.GetOne(id);
            return View(treat);
        }


        // POST: Treatment/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Treatment treat)
        {
            try
            {
                // TODO: Add delete logic here
                service.TreatmentGateway.Delete(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        private IEnumerable<SelectListItem> GetTherapists()
        {
            var allTheras = service.TherapistGateway.GetAll()
                        .Select(x =>
                                new SelectListItem
                                {
                                    Value = x.Id.ToString(),
                                    Text = x.Name
                                });

            return new SelectList(allTheras, "Value", "Text");
        }
        private IEnumerable<SelectListItem> GetTypes()
        {
            var allTypes = service.TreatmentTypeGateway.GetAll()
                        .Select(x =>
                                new SelectListItem
                                {
                                    Value = x.Id.ToString(),
                                    Text = x.Name,
                                });

            return new SelectList(allTypes, "Value", "Text");
        }
    }
}
