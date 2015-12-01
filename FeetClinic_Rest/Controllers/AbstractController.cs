using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DomainModel.BE;
using DomainModel.BLL.Interfaces;
using FeetClinic_DAL.Conrete;

namespace FeetClinic_Rest.Controllers
{
    public class AbstractController<TEntity> : ApiController where TEntity:IEntity
    {
        DalFacade facade = new DalFacade();

        // GET: api/Addresses
        public HttpResponseMessage GetAddresses()
        {
            try
            {
                IEnumerable<Address> addresses = facade.Addresses.GetAll();
                if (addresses.Count() > 0)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, addresses);
                }
                return Request.CreateResponse(HttpStatusCode.NotFound, "Addresses not found");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // GET: api/Addresses/5
        public HttpResponseMessage GetAddress(int id)
        {
            try
            {
                Address address = facade.Addresses.GetOne(a => a.Id == id);
                if (address == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Address not found");
                }

                return Request.CreateResponse(HttpStatusCode.OK, address);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

        }

        // PUT: api/Addresses/5
        public HttpResponseMessage PutAddress(int id, Address address)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
            }
            if (id != address.Id)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Addresses id is not the same with given id");
            }

            try
            {
                facade.Addresses.Update(address);
                facade.Save();
            }
            catch (Exception ex)
            {
                if (!AddressExists(id))
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Address with given id not found");
                }
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
            return Request.CreateResponse(HttpStatusCode.OK, address);
        }

        // POST: api/Addresses
        public HttpResponseMessage PostAddress(Address address)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
            }
            try
            {
                address = facade.Addresses.Create(address);
                facade.Save();
                var response = Request.CreateResponse(HttpStatusCode.Created, address);

                string uri = Url.Link("DefaultApi", new { id = address.Id });
                response.Headers.Location = new Uri(uri);

                return response;
            }
            catch (Exception ex)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // DELETE: api/Addresses/5
        public HttpResponseMessage DeleteAddress(int id)
        {
            try
            {
                Address address = facade.Addresses.Delete(id);
                facade.Save();
                return Request.CreateResponse(HttpStatusCode.OK, address);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        protected override void Dispose(bool disposing)
        {
            facade.Dispose();
            base.Dispose(disposing);
        }

        private bool AddressExists(int id)
        {
            return facade.Addresses.Any(c => c.Id == id);
        }

    }
}
