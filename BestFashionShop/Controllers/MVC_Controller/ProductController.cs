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
            return View();
        }
        public ActionResult EditProduct()
        {
            return View();
        }
    }
}