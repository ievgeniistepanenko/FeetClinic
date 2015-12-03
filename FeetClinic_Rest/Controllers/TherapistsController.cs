using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BLL.Managers;
using DomainModel.BE;

namespace FeetClinic_Rest.Controllers
{
    public class TherapistsController : AbstractApiController<Therapist,TherapistsManager>
    {
        public TherapistsController()
        {
            Manager = BllFacade.TherapistsManager;
        }

        protected override TherapistsManager GetManager()
        {
            return Manager;
        }
        public HttpResponseMessage GetTherapists()
        {
            return GetAll();
        }

        // GET: api/Therapists/5
        public HttpResponseMessage GetTherapist(int id)
        {
            return GetTherapist(id, "");
        }
        public HttpResponseMessage GetTherapist(int id, string properties)
        {
            return GetOne(id, properties);
        }

        // PUT: api/Therapists/5
        public HttpResponseMessage PutTherapist(int id, Therapist therapist)
        {
            return UpdateOne(id, therapist);
        }

        // POST: api/Therapist
        public HttpResponseMessage PostTherapist(Therapist therapist)
        {
            return CreateOne(therapist);
        }

        // DELETE: api/Therapists/5
        public HttpResponseMessage DeleteTherapist(int id)
        {
            return DeleteOne(id);
        }
    }
}
