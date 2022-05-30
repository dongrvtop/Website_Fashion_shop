using BestFashionShop.DAL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BestFashionShop.Controllers.MVC_Controller
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult CreateProduct()
        {
            if(Session["RoleUser"].ToString() != "ADMIN")
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        public ActionResult ManageProduct()
        {
            if (Session["RoleUser"].ToString() != "ADMIN")
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        public ActionResult EditProduct()
        {
            return View();
        }
        public ActionResult DetailProduct()
        {
            return View();
        }
        public ActionResult ListProduct()
        {
            return View();
        }
    }
}