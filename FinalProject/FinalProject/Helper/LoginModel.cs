using FinalProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalProject.Helper
{
    public class LoginHelper
    {

        public static void LogOff()
        {
            HttpContext.Current.Session["user"] = null;
        }

        public static User CurrentUser()
        {
            return (User)HttpContext.Current.Session["user"];
        }

        public static bool IsLoggedIn()
        {
            return (User)HttpContext.Current.Session["user"] != null;
        }

        public static void CreateUser(User user)
        {
            HttpContext.Current.Session["user"] = user;
        }
    }
}