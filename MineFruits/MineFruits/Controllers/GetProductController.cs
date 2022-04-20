using MineFruits.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MineFruits.Controllers
{
    public class GetProductController : Controller
    {
        
        private MineFruitsDB db = new MineFruitsDB();

        public ActionResult Index(int? page)
        {
            var products = db.Products.ToList();
            int pageNumber = (page ?? 1);

            return View(products.ToPagedList(pageNumber, 9));
        }
    }
}