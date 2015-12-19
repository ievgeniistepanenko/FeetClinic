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
    [Authorize(Roles = "admin")]
    public class TreatmentController : Controller
    {
        private readonly ServiceGatewayFactory service;

        public TreatmentController()
        {
            service = new ServiceGatewayFactory();
        }
        [AllowAnonymous]
        // GET: Treatment
        public ActionResult Index(string treatmentType)
        {
            IEnumerable<Treatment> treat = service.TreatmentGateway.GetAll("TreatmentType");
            ViewBag.TypeList = new SelectList(
                    service.TreatmentTypeGateway.GetAll().OrderBy(tt => tt.Name)
                        .Select(tt => tt.Name));
            if (string.IsNullOrEmpty(treatmentType))
            {
                return View(treat);
            }
            else
            {
                return View(treat.Where(t => t.TreatmentType.Name == treatmentType));
            }
            
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
                    Therapists = allThera
                };

                service.TreatmentGateway.CreateOne(treatment);
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
                return View(model);
            }
        }


        // GET: Treatment/Delete/5
        public ActionResult Delete(int id)
        {
           
            service.TreatmentGateway.Delete(id);
            return RedirectToAction("Index");
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
