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
    [Authorize(Roles = "admin")]
    public class TherapistController : Controller
    {
        private readonly ServiceGatewayFactory service;


        public TherapistController()
        {
            service = new ServiceGatewayFactory();
        }
        // GET: Therapist
        [AllowAnonymous]
        public ActionResult Index()
        {
            IEnumerable<Therapist> therapists = service.TherapistGateway.GetAll();
            return View(therapists);
        }

        // GET: Therapist/Create
        public ActionResult Create()
        {
            var model = new CreateTherapistViewModel()
            {
                TreatmentsSelectListItems = GetTreatmentForSelectedList(),
                
            };
            return View(model);
        }

        // POST: Therapist/Create
        [HttpPost]
        public ActionResult Create(CreateTherapistViewModel model)
        {

                if (ModelState.IsValid)
                {
                List<Treatment> treats = new List<Treatment>();
                    model.SelectedTreatmentId = model.SelectedTreatmentId ?? new int[0];
                foreach (int i in model.SelectedTreatmentId)
                    {
                        treats.Add(service.TreatmentGateway.GetOne(i));
                    }
                Therapist therapist = new Therapist
                {
                    Name = model.Name,
                    Description = model.Description,
                    WorkingHourses = model.WorkingHourses,
                    Treatments = treats
                };

                service.TherapistGateway.CreateOne(therapist);
                return RedirectToAction("Index");
                }
                return View();

        }
        [AllowAnonymous]
        // GET: Therapist/Edit/5
        public ActionResult Edit(int id)
        {
            Therapist therapist = service.TherapistGateway.GetOne(id, "Treatments,WorkingHourses");
            if (therapist == null)
            {
                return HttpNotFound();
            }

            CreateTherapistViewModel model = new TherapistViewModel()
            {
                Description = therapist.Description,
                Name = therapist.Name,
                TreatmentsSelectListItems = GetTreatmentForSelectedList(therapist.Treatments),
                WorkingHourses = therapist.WorkingHourses,
                Id = therapist.Id
            };

            return View(model);
        }

        // POST: Therapist/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, TherapistViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    List<Treatment> treats = new List<Treatment>();
                    model.SelectedTreatmentId = model.SelectedTreatmentId ?? new int[0];
                    foreach (int i in model.SelectedTreatmentId)
                    {
                        treats.Add(service.TreatmentGateway.GetOne(i));
                    }
                    Therapist therapist = new Therapist
                    {
                        Id = id,
                        Name = model.Name,
                        Description = model.Description,
                        Treatments = treats
                    };
                    service.TherapistGateway.Update(therapist);
                    return RedirectToAction("Index");

                }
                return View(model);
            }
            catch
            {
                return View(model);
            }
        }

        // GET: Therapist/Delete/5
        public ActionResult Delete(int id)
        {
            service.TherapistGateway.Delete(id);
            return RedirectToAction("Index");
        }

        

        private SelectList GetTreatmentForSelectedList()
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

        private SelectList GetTreatmentForSelectedList(List<Treatment> existing )
        {
            var treats = service.TreatmentGateway.GetAll()
                        .Select(x =>
                                new SelectListItem
                                {
                                    Value = x.Id.ToString(),
                                    Text = x.Name,
                                    Selected = existing.Any(t=>t.Id == x.Id)
                                    
                                });


            return new SelectList(treats, "Value", "Text");
        }


    }
}
