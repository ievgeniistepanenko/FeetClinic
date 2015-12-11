using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BE.BE.Treatments;
using FeetClinic.WEB.ServiceGateway;

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

        // GET: Treatment
        public ActionResult IndexType()
        {
            return View();
        }

        // GET: Treatment/Details/5
        public ActionResult Details(int id)
        {
            Treatment treat = service.TreatmentGateway.GetOne(id);

            if (treat == null)
            {
                return HttpNotFound();
            }
            return View(treat);
        }

        // GET: Treatment/Details/5
        public ActionResult DetailsType(int id)
        {
            return View();
        }

        // GET: Treatment/Create
        public ActionResult Create()
        {
            return View();
        }

        // GET: Treatment/Create
        public ActionResult CreateType()
        {
            return View();
        }

        // POST: Treatment/Create
        [HttpPost]
        public ActionResult Create(Treatment treat)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    service.TreatmentGateway.CreateOne(treat);
                    return RedirectToAction("Index");
                }
                return View(treat);
            }
            catch
            {
                return View();
            }
        }

        // POST: Treatment/Create
        [HttpPost]
        public ActionResult CreateType(FormCollection collection)
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

        // GET: Treatment/Edit/5
        public ActionResult Edit(int id)
        {
            Treatment treat = service.TreatmentGateway.GetOne(id);
            if (treat == null)
            {
                return HttpNotFound();
            }
            return View(treat);
        }

        // GET: Treatment/Edit/5
        public ActionResult EditType(int id)
        {
            return View();
        }

        // POST: Treatment/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Treatment treat)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    service.TreatmentGateway.Update(treat);

                    return RedirectToAction("Index");
                }
                return View(treat);
            }
            catch
            {
                return View();
            }
        }

        // POST: Treatment/Edit/5
        [HttpPost]
        public ActionResult EditType(int id, FormCollection collection)
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

        // GET: Treatment/Delete/5
        public ActionResult Delete(int id)
        {
            Treatment treat = service.TreatmentGateway.GetOne(id);
            return View(treat);
        }

        // GET: Treatment/Delete/5
        public ActionResult DeleteType(int id)
        {
            return View();
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

        // POST: Treatment/Delete/5
        [HttpPost]
        public ActionResult DeleteType(int id, FormCollection collection)
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
