using BE.BE.Treatments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BE.Interfaces;
using FeetClinic.WEB.ServiceGateway;

namespace FeetClinic.WEB.Controllers
{
    [Authorize(Roles = "admin")]
    public class TreatmentTypesController : Controller
    {

        private readonly ServiceGatewayFactory service;

        
        public TreatmentTypesController()
        {
            service = new ServiceGatewayFactory();
        }
        
        // GET: TreatmentTypes
        public ActionResult Index()
        {
            IEnumerable<TreatmentType> types = service.TreatmentTypeGateway.GetAll();
            return View(types);
        }

        // GET: TreatmentTypes/Details/5
        public ActionResult Details(int id)
        {
            TreatmentType type = service.TreatmentTypeGateway.GetOne(id);

            if (type == null)
            {
                return HttpNotFound();
            }
            return View(type);
        }

        // GET: TreatmentTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TreatmentTypes/Create
        [HttpPost]
        public ActionResult Create(TreatmentType type)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    service.TreatmentTypeGateway.CreateOne(type);
                    return RedirectToAction("Index");
                }
                return View(type);
            }
            catch
            {
                return View();
            }
        }

        // GET: TreatmentTypes/Edit/5
        public ActionResult Edit(int id)
        {
            TreatmentType type = service.TreatmentTypeGateway.GetOne(id);
            if (type == null)
            {
                return HttpNotFound();
            }
            return View(type);
        }

        // POST: TreatmentTypes/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, TreatmentType type)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    service.TreatmentTypeGateway.Update(type);

                    return RedirectToAction("Index");
                }
                return View(type);
            }
            catch
            {
                return View();
            }
        }

        // GET: TreatmentTypes/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                service.TreatmentTypeGateway.Delete(id);
                return RedirectToAction("Index");
            }
            catch
            {
                TreatmentType type = service.TreatmentTypeGateway.GetOne(id);
                return View(type);
            }
            
        }
    }
}
