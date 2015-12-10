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
    public class TreatmentTypesController : Controller
    {

        ServiceGateway<TreatmentType> service = new ServiceGateway<TreatmentType>("api/TreatmentTypes/");

        // GET: TreatmentTypes
        public ActionResult Index()
        {
            IEnumerable<TreatmentType> types = service.GetAll();
            return View(types);
        }

        // GET: TreatmentTypes/Details/5
        public ActionResult Details(int id)
        {
            TreatmentType type = service.GetOne(id);

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
                    service.CreateOne(type);
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
            TreatmentType type = service.GetOne(id);
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
                    service.Update(type);

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
            TreatmentType type = service.GetOne(id);
            return View(type);
        }

        // POST: TreatmentTypes/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, TreatmentType type)
        {
            try
            {
                // TODO: Add delete logic here
                service.Delete(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
