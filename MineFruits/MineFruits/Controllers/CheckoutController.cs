using MineFruits.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace MineFruits.Controllers
{
    public class CheckoutController : Controller
    {
        private const string Cartsession = "Cartsession";
        MineFruitsDB db = new MineFruitsDB();
        // GET: Checkout
        public ActionResult Index()
        {
            var cart = Session[Cartsession];
            if (Session["TenTaiKhoan"] != null)
            {
                var list = new List<CartItem>();
                if (cart != null)
                {
                    list = (List<CartItem>)cart;
                }
                return View(list);
            }
            else
            {
                return RedirectToAction("Login", "Profile");
            }
        }
        public ActionResult Additem(int productId, int quantity)
        {
            var product = db.Products.Find(productId);
            var cart = Session[Cartsession];
            if (product.Amount == 0 || product.Amount < 0)
            {
                return RedirectToAction("ErrorOrder", "Checkout");
            }
            else
            {
                if (cart != null)
                {
                    var list = (List<CartItem>)cart;
                    if (list.Any(x => x.Product.ProductID == productId))
                    {
                        foreach (var item in list)
                        {
                            if (item.Product.ProductID == productId)
                            {
                                item.Amount += quantity;
                            }
                        }
                    }
                    else
                    {
                        //tạo mới đối tượng cart item;
                        var item = new CartItem();
                        item.Product = product;
                        item.Amount = quantity;
                        list.Add(item);


                    }
                    Session[Cartsession] = list;
                }
                else
                {
                    var item = new CartItem();
                    item.Product = product;
                    item.Amount = quantity;
                    var list = new List<CartItem>();
                    list.Add(item);
                    //gắn với session
                    Session[Cartsession] = list;

                }
            }

            return RedirectToAction("Index", "Checkout");
        }
        public JsonResult Update(string cartModel)
        {

            var jsonCart = new JavaScriptSerializer().Deserialize<List<CartItem>>(cartModel);
            var sessionCart = (List<CartItem>)Session[Cartsession];
            foreach (var item in sessionCart)
            {
                var jsonItem = jsonCart.SingleOrDefault(x => x.Product.ProductID == item.Product.ProductID);
                if (jsonItem != null)
                {
                    item.Amount = jsonItem.Amount;
                }
            }
            Session[Cartsession] = sessionCart;
            return Json(new
            {
                status = true
            });
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var sessionCart = (List<CartItem>)Session[Cartsession];
            sessionCart.RemoveAll(x => x.Product.ProductID == id);
            Session[Cartsession] = sessionCart;
            return RedirectToAction("Index", "Checkout");
        }
        public ActionResult DeleteAll()
        {
            Session.Remove(Cartsession);
            return RedirectToAction("Index", "Checkout");
        }
        [HttpGet]
        public ActionResult Order(string sumTotal)
        {
            ViewBag.sumTotal = sumTotal;
            var cart = Session[Cartsession];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }
            return View(list);
        }

        [HttpPost]
        public ActionResult Order(string DcNhanHang, string GhiChu)
        {
            List<CartItem> ListSP = new List<CartItem>();
            if (Session["Cartsession"] != null)
            {
                ListSP = (List<CartItem>)Session["Cartsession"];
            }
            if (Session["TenTaiKhoan"] != null)
            {
                Cart giohang = new Cart();
                giohang.Username = (string)Session["TenTaiKhoan"];
                db.Carts.Add(giohang);
                db.SaveChanges();
                int generatedId = giohang.CartID;
                foreach (var item in ListSP)
                {
                    CartDetail ct = new CartDetail();
                    ct.CartID = generatedId;
                    ct.ProductID = item.Product.ProductID;
                    ct.Amount = item.Amount;
                    ct.Price = item.Product.Price;
                    db.CartDetails.Add(ct);
                    db.SaveChanges();
                    var product = db.Products.SingleOrDefault(x => x.ProductID == item.Product.ProductID);
                    product.Amount -= item.Amount;

                }
                Order hd = new Order();
                hd.CreateDate = DateTime.Now;
                hd.Status = "Chờ xác nhận";
                hd.FeeShip = 25000;
                hd.Note = GhiChu;
                hd.CartID = generatedId;
                if (DcNhanHang != "")
                {
                    hd.Address = DcNhanHang;
                }
                else
                {
                    Account tk = Session["TaiKhoan"] as Account;
                    hd.Address = tk.Address;
                }

                //db.Orders.Add(hd);
                //db.SaveChanges();

                try
                {
                    db.Orders.Add(hd);
                    db.SaveChanges();
                }
                catch (DbEntityValidationException dbEx)
                {
                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            System.Console.WriteLine("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                        }
                    }
                }


            }
            else
            {
                return RedirectToAction("Login", "Profile");
            }
            Session[Cartsession] = null;
            return Redirect("/");
        }

        public ActionResult ErrorOrder()
        {
            try
            {
                var sessionUser = Session["TaiKhoan"] as Account;
                ViewBag.name = sessionUser.Name;
                ViewBag.userName = sessionUser.Username;
                var ss = ViewBag.name;
                if (ViewBag.name != null)
                {
                    ViewBag.name = sessionUser.Name;
                }
                else
                {
                    ViewBag.name = null;
                }
            }
            catch
            {
                ViewBag.name = null;
            }
            return View();
        }
    }
}