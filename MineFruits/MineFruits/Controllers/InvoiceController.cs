using MineFruits.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MineFruits.Controllers
{
    public class InvoiceController : Controller
    {
        MineFruitsDB db = new MineFruitsDB();
        // GET: Invoice
        public ActionResult DetailReceipt(int maHD)
        {
            Order hd = db.Orders.Find(maHD);
            return View(hd);
        }
    }
}