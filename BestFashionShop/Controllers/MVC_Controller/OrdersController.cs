using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BestFashionShop.Controllers.MVC_Controller
{
    public class OrdersController : Controller
    {
        // GET: Orders
        public ActionResult Index()
        {
            if (Session["RoleUser"].ToString() != "USER")
            {
                return RedirectToAction("Index", "Home");
            } 
            return View();
        }
        public ActionResult MyOrder()
        {
            if (Session["RoleUser"].ToString() != "USER")
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        public ActionResult ManageOrders()
        {
            if (Session["RoleUser"].ToString() != "ADMIN")
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        public ActionResult DetailOrders()
        {
            if (Session["RoleUser"].ToString() != "ADMIN")
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
    }
}