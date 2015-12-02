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
    public class AddressesController : AbstractApiController<Address, AddressManager>
    {
        public AddressesController()
        {
            Manager = BllFacade.AddressManager;
        }
        protected override AddressManager GetManager()
        {
            return Manager;
        }

        public HttpResponseMessage GetAddresses()
        {
            return GetAll();
        }

        // GET: api/Addresses/5
        public HttpResponseMessage GetAddress(int id)
        {
            return GetAddress(id,"");
        }
        public HttpResponseMessage GetAddress(int id,string properties)
        {
            return GetOne(id,properties);
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

