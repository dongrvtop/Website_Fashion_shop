using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BestFashionShop.Controllers.MVC_Controller
{
    public class CartController : Controller
    {
        // GET: Cart
        public ActionResult Index()
        {
            if (Session["RoleUser"].ToString() != "USER")
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
    }
}