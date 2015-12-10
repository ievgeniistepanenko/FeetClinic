using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BE.BE.Customer;
using BE.BE.Identity;
using FeetClinic.WEB.Models;
using FeetClinic.WEB.ServiceGateway;
using FeetClinic_UserDAL.Concrete;

namespace FeetClinic.WEB.Controllers
{
    public class ManageController : Controller
    {
        private readonly UserDalFacade _facade = new UserDalFacade();
        private readonly ServiceGatewayFactory _serviceFactory = new ServiceGatewayFactory();
        // GET: Manage
        public ActionResult Index(int userId,string returnUrl)
        {
            User user = _facade.Users.GetOne(u => u.Id == userId);
            CustomerProfile customer = _serviceFactory.CustomersGateway.GetOne(userId, "Address");
            RegisterViewModel  customerModel= new RegisterViewModel(user,customer);

            return View(customerModel);
        }
    }
}