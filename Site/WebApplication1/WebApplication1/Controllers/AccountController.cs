﻿using Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace AhdIran.Controllers
{
    public class AccountController : Controller
    {
        private DatabaseContext db = new DatabaseContext();
        [Route("login")]
        public ActionResult Login(string ReturnUrl = "")
        {
            ViewBag.Message = "";
            ViewBag.ReturnUrl = ReturnUrl;

            return View();

        }

        [Route("login")]
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                User oUser = db.Users.Include(u => u.Role).FirstOrDefault(a => a.CellNum == model.Username && a.Password == model.Password);

                if (oUser != null)
                {
                    var ident = new ClaimsIdentity(
                      new[] { 
              // adding following 2 claim just for supporting default antiforgery provider
              new Claim(ClaimTypes.NameIdentifier, oUser.CellNum),
              new Claim("http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider", "ASP.NET Identity", "http://www.w3.org/2001/XMLSchema#string"),
              new Claim(ClaimTypes.Name,oUser.Id.ToString()),

              // optionally you could add roles if any
               new Claim(ClaimTypes.Role, oUser.Role.Name),

                      },
                      DefaultAuthenticationTypes.ApplicationCookie);

                    HttpContext.GetOwinContext().Authentication.SignIn(
                       new AuthenticationProperties { IsPersistent = true }, ident);
                    return RedirectToLocal(returnUrl, oUser.Role.Name); // auth succeed 
                }
                else
                {
                    // invalid username or password
                    TempData["WrongPass"] = "نام کاربری و یا کلمه عبور وارد شده صحیح نمی باشد.";
                }
            }
    

            return View(model);
        }

        private ActionResult RedirectToLocal(string returnUrl, string role)
        {
            
                if (role == "Administrator")
                    return RedirectToAction("index", "Users");

            if (role == "supervisor")
                return RedirectToAction("SupervisorIndex", "Projects");

            if (role == "promoter")
                return RedirectToAction("PromoterDailyPlanAction", "DailyPromoterPlans");
            //if(role== "Administrator")
            //return RedirectToAction("Index", "users");
            //else if(role=="company")
            //    return RedirectToAction("Details", "WbsRequirments");


            //return RedirectToAction("Index", "users");

            return RedirectToAction("login", "Account");
             
        }
        public ActionResult LogOff()
        {
            if (User.Identity.IsAuthenticated)
            {
                HttpContext.GetOwinContext().Authentication.SignOut();
            }
            return RedirectToAction("login","Account");
        }
    }
}