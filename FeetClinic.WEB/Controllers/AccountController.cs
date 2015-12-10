﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using BE.BE.Identity;
using FeetClinic.WEB.Models;
using FeetClinic_UserDAL.Concrete;
using Microsoft.Owin.Security;
using System.Data.Entity;
using System.Net;
using System.Net.Http;
using BE.BE.Customer;
using FeetClinic.WEB.ServiceGateway;

namespace FeetClinic.WEB.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly UserDalFacade _facade = new UserDalFacade();
        private IAuthenticationManager AuthenticationManager => 
            HttpContext.GetOwinContext().Authentication;

        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(AccountViewModels.RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                Role role = _facade.Roles.GetOne(r => r.Name == "user");
                User user = new User {Email = model.Email,Password = model.Password,
                    Phone = model.Phone,Role = role};

                _facade.Users.Create(user);
                _facade.Save();

                User userWithId = _facade.Users.GetOne(u => u.Email ==user.Email && u.Password == user.Password 
                                    && u.Phone == user.Phone && u.RoleId == role.Id );
                if (userWithId.Id != 0)
                {
                    Address address = new Address
                    {
                        City = model.City,
                        StreetName = model.StreetName,
                        StreetNumber = model.StreetNumber,
                        ZipCode = model.ZipCode
                    };
                    CustomerProfile customer = new CustomerProfile
                    {
                        Address = address,
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        Id = userWithId.Id
                    };
                    ServiceGateway<CustomerProfile> serviceGateway = new ServiceGateway<CustomerProfile>("api/Customers");
                    HttpResponseMessage response = serviceGateway.CreateOne(customer);
                    if (response.StatusCode == HttpStatusCode.Created)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    _facade.Users.Delete(userWithId);
                    _facade.Save();
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }



        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }
        

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(AccountViewModels.LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                int phone;
                User user;
                if (int.TryParse(model.LogIn,out phone))
                {
                    user = _facade.Users.GetOne(u => u.Phone == phone, "Role");
                }
                else
                {
                    user = _facade.Users.GetOne(u => u.Email == model.LogIn && u.Password == model.Password, "Role");
                }
                
                if (user == null)
                {
                    ModelState.AddModelError("", "Password or login name incorrect");
                }
                else
                {
                    ClaimsIdentity claim = new ClaimsIdentity("ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
                    claim.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString(), ClaimValueTypes.String));
                    claim.AddClaim(new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email, ClaimValueTypes.String));
                    claim.AddClaim(new Claim("http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider",
                        "OWIN Provider", ClaimValueTypes.String));
                    if (user.Role != null)
                        claim.AddClaim(new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role.Name, ClaimValueTypes.String));

                    AuthenticationManager.SignOut();
                    AuthenticationManager.SignIn(new AuthenticationProperties
                    {
                        IsPersistent = true
                    }, claim);
                    return RedirectToLocal(returnUrl);
                }
            }
            return View(model);
        }

        public ActionResult Logout()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

    }
}