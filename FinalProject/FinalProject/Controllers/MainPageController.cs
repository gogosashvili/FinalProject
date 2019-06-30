using FinalProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FinalProject.Controllers
{
    public class MainPageController : Controller
    {
        MainPageDataProvider mainPageDataProvider = new MainPageDataProvider();

        // GET: MainPage
        public ActionResult Index()
        {
            ViewBag.FreeBooks = mainPageDataProvider.GetFreeBooks().ToList();
            ViewBag.SwapBoos = mainPageDataProvider.GetSwapBooks().ToList();
            ViewBag.PaidBooks = mainPageDataProvider.GetPaidBooks().ToList();

            return View();
        }
    }
}