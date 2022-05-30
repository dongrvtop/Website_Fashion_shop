using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BestFashionShop.Controllers.MVC_Controller
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ManageUser()
        {
            if (Session["RoleUser"].ToString() != "ADMIN")
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        public ActionResult DetailUser()
        {
            if (Session["RoleUser"].ToString() != "ADMIN")
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
    }
}