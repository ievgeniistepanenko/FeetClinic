using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DomainModel.BE;
using FeetClinic_DAL.Conrete;

namespace FeetClinic_Rest.Controllers
{
    public class AddressesController : AbstractApiController<Address>
    {
        public AddressesController()
        {
            Repository = Facade.Addresses;
        }

        public HttpResponseMessage GetAddresses()
        {
            return GetAll();
        }

        // GET: api/Addresses/5
        public HttpResponseMessage GetAddress(int id)
        {
            return GetOne(id);

        }

        // PUT: api/Addresses/5
        public HttpResponseMessage PutAddress(int id, Address address)
        {
            return UpdateOne(id, address);
        }

        // POST: api/Addresses
        public HttpResponseMessage PostAddress(Address address)
        {
            return CreateOne(address);
        }

        // DELETE: api/Addresses/5
        public HttpResponseMessage DeleteAddress(int id)
        {
            return DeleteOne(id);
        }
    }
}

