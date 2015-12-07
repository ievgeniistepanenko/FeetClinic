using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BLL.Managers;
using DomainModel.BE;
using DomainModel.BE.Customer;
using DomainModel.BE.Treatments;

namespace FeetClinic_Rest.Controllers
{
    public class TreatmentTypesController : AbstractApiController<TreatmentType,TreatmentTypesManager>
    {
        public TreatmentTypesController()
        {
            Manager = BllFacade.TreatmentTypesManager;
        }

        protected override TreatmentTypesManager GetManager()
        {
            return Manager;
        }
        public HttpResponseMessage GetTreatmentTypes()
        {
            return GetAll();
        }

        // GET: api/TreatmentTypes/5
        public HttpResponseMessage GetTreatmentType(int id)
        {
            return GetTreatmentType(id, "");
        }
        public HttpResponseMessage GetTreatmentType(int id, string properties)
        {
            return GetOne(id, properties);
        }

        // PUT: api/TreatmentTypes/5
        public HttpResponseMessage PutTreatmentType(int id, TreatmentType type)
        {
            return UpdateOne(id, type);
        }

        // POST: api/TreatmentTypes
        public HttpResponseMessage PostTreatmentType(TreatmentType type)
        {
            return CreateOne(type);
        }

        // DELETE: api/TreatmentTypes/5
        public HttpResponseMessage DeleteTreatmentType(int id)
        {
            return DeleteOne(id);
        }
    }
}
