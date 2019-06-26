using FinalProject.Helper;
using FinalProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FinalProject.Controllers
{
    public class AccountController : Controller
    {
        AccountDataProvider accountDataProvider = new AccountDataProvider();
       
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                accountDataProvider.add(new User()
                {
                    Name = model.Name,
                    Surname = model.Surname,
                    Email = model.Email,
                    //Password = SHA.GenerateSHA12Stirng(model.Password),
                    Password = model.Password,
                    PhoneNumber = model.PhoneNumber,
                    IsAdmin = 0,
                    IsActive = 1
                });
            }
            if (ModelState.IsValid && accountDataProvider.validLogin(new LoginViewModel()
            { Email = model.Email, Password = model.Password }))
            {
                return RedirectToAction("Index", "Account");
            }
            return View(model);
        }

        public ActionResult Edit(int id)
        {
            return View(accountDataProvider.GetUserById(id));
        }

        [HttpPost]
        public ActionResult Edit(User model)
        {
            accountDataProvider.Edit(model);
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            return View(accountDataProvider.GetUserById(id));
        }

        [HttpPost]
        public ActionResult Delete(User model)
        {
            accountDataProvider.Delete(model);
            return RedirectToAction("Index");
        }


        BookEntities _db = new BookEntities();


        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(LoginViewModel user)
        {
            if (ModelState.IsValid && accountDataProvider.validLogin(user))
            {
                var result = _db.Users.FirstOrDefault(e => e.Email == user.Email);
                if (result.IsAdmin == 1)
                {
                    return RedirectToAction("Index", "Admin");
                }
                else if (result.IsAdmin == 0)
                {
                    //იუზერიც ექშენზე რედირექთი არ გამომრჩეს :დ 
                    return RedirectToAction("Index", "Account");
                }
               
            }
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            LoginHelper.LogOff();

            return RedirectToAction("Login", "Account");
        }

       
    }
}