using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BLL.Managers;
using DomainModel.BE;
using DomainModel.BE.Treatment;

namespace FeetClinic_Rest.Controllers
{
    public class TreatmentsController : AbstractApiController<Treatment,TreatmentsManager>
    {
        public TreatmentsController()
        {
            Manager = BllFacade.TreatmentsManager;
        }

        protected override TreatmentsManager GetManager()
        {
            return Manager;
        }
        public HttpResponseMessage GetTreatments()
        {
            return GetAll();
        }

        // GET: api/Treatments/5
        public HttpResponseMessage GetTreatment(int id)
        {
            return GetTreatment(id, "");
        }
        public HttpResponseMessage GetTreatment(int id, string properties)
        {
            return GetOne(id, properties);
        }

        // PUT: api/Treatments/5
        public HttpResponseMessage PutTreatment(int id, Treatment treatment)
        {
            return UpdateOne(id, treatment);
        }

        // POST: api/Treatments
        public HttpResponseMessage PostTreatmentType(Treatment treatment)
        {
            return CreateOne(treatment);
        }

        // DELETE: api/Treatments/5
        public HttpResponseMessage DeleteTreatment(int id)
        {
            return DeleteOne(id);
        }


    }
}
