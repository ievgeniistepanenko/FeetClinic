using BE.BE.Treatments;
using FeetClinic.WEB.ServiceGate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BE.Interfaces;

namespace FeetClinic.WEB.Controllers
{
    public class TreatmentTypesController : Controller
    {

        ServiceGateway<TreatmentType> service = new ServiceGateway<TreatmentType>("");

        // GET: TreatmentTypes
        public ActionResult Index()
        {
            return View();
        }

        // GET: TreatmentTypes/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: TreatmentTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TreatmentTypes/Create
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

        // GET: TreatmentTypes/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: TreatmentTypes/Edit/5
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

        // GET: TreatmentTypes/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TreatmentTypes/Delete/5
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
