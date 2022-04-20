using MineFruits.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MineFruits.Controllers
{
    public class HomeController : Controller
    {
        MineFruitsDB db = new MineFruitsDB();
        //public ActionResult Index(int? page)
        //{
        //    var products = db.Products.OrderByDescending(p => p.ProductID);
        //    int pageNumber = (page ?? 1);

        //    return View(products.ToPagedList(pageNumber, 8));
        //}

        public ActionResult Index()
        {
            //var products = db.Products.OrderByDescending(p => p.ProductID);
            var products = db.Products.ToList();
            return View(products);
        }

        [HttpGet]
        public ActionResult CancelOrder(int maHD)
        {
            Order hd = db.Orders.Find(maHD);
            hd.Status = "Đã hủy";
            db.SaveChanges();
            return RedirectToAction("Index", "Profile", new { TenTK = Session["TenTaiKhoan"] });
        }


        public PartialViewResult _DC_GiaoHang()
        {
            Account taiKhoan = (Account)Session["TaiKhoan"];
            if (taiKhoan == null)
            {
                return PartialView();
            }
            else
            {
                return PartialView(taiKhoan);
            }
        }
        public PartialViewResult _DC_LienHe()
        {
            Account taiKhoan = (Account)Session["TaiKhoan"];
            if (taiKhoan == null)
            {
                return PartialView();
            }
            else
            {
                return PartialView(taiKhoan);
            }
        }
    }
}