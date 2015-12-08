using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BE.BE.Customer;
using BLL.Managers;


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
        /// <summary>
        /// Returns all addresses
        /// </summary>
        /// <returns>List of Addresses</returns>
        public HttpResponseMessage GetAddresses()
        {
            return GetAll();
        }

        /// <summary>
        /// Finds a address by Id
        /// </summary>
        /// <param name="id">Addres Id</param>
        /// <returns>Http response message containing the Address and a HTTP status code</returns>
        // GET: api/Addresses/5
        public HttpResponseMessage GetAddress(int id)
        {
            return GetOne(id);
        }


        /// <summary>
        /// Replaces a address with a new address
        /// </summary>
        /// <param name="id">Address Id of the address to replace</param>
        /// <param name="address">The new address</param>
        /// <returns>Http response message containing the Address and a HTTP status code</returns>
        // PUT: api/Addresses/5
        public HttpResponseMessage PutAddress(int id, Address address)
        {
            return UpdateOne(id, address);
        }

        /// <summary>
        /// Creates a new address 
        /// </summary>
        /// <param name="address">Address</param>
        /// <returns>Http response message containing the Address, its URI, and a HTTP status code</returns>
        // POST: api/Addresses
        public HttpResponseMessage PostAddress(Address address)
        {
            return CreateOne(address);
        }

        /// <summary>
        /// Deletes a address
        /// </summary>
        /// <param name="id">Address Id of the address that sould be deleted</param>
        /// <returns>Http response message containing the Address and HTTP status code</returns>
        // DELETE: api/Addresses/5
        public HttpResponseMessage DeleteAddress(int id)
        {
            return DeleteOne(id);
        }
    }
}

