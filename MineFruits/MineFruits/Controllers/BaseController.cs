using MineFruits.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MineFruits.Controllers
{
    public class BaseController : ProtectController
    {
        private MineFruitsDB db = new MineFruitsDB();

        private const string Cartsession = "Cartsession";

        [NotAuthorize]
        public PartialViewResult _HeaderTop()
        {
            
            return PartialView(user);
        }
        [NotAuthorize]
        public PartialViewResult _HeaderMid()
        {
            var carItem = Session["Cartsession"] as List<CartItem>;
            if (carItem != null)
            {
                ViewBag.cartpronumber = carItem.Count;
            }
            else
            {
                ViewBag.cartpronumber = 0;
            }
            return PartialView(user);
        }
        [NotAuthorize]
        public PartialViewResult _Categories()
        {
            var categories = db.Categories.ToList();
            return PartialView(categories);
        }
        [NotAuthorize]
        public PartialViewResult _Search()
        {

            return PartialView();
        }

        public PartialViewResult _OrderView(string TenTK)
        {
            var carts = db.Carts.Where(g => g.Username.Equals(TenTK)).ToList();
            var hds = db.Orders;
            var receipts = from x in carts join y in hds on x.CartID equals y.CartID select y;
            return PartialView(receipts);


        }
    }
}