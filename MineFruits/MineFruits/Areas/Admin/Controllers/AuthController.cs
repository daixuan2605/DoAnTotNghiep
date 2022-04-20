using MineFruits.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MineFruits.Areas.Admin.Controllers
{
    public class AuthController : Controller
    {
        MineFruitsDB db = new MineFruitsDB();
        // GET: Admin/Auth

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string userName, string password)
        {
                if (string.IsNullOrEmpty(userName))
            {
                ViewBag.ErrorUsername = "Tên tài khoản không được để trống";
            }
            if (string.IsNullOrEmpty(password))
            {
                ViewBag.ErrorPassword = "Mật khẩu không được để trống";

            }
            var TaiKhoan = db.Accounts.Where(u => u.Username.Equals(userName) && u.Password.Equals(password) && u.Status == true).ToList();
            if (TaiKhoan.Count() > 0)
            {
                if (TaiKhoan.First().Power == 0)
                {
                    ViewBag.FailedMessage = "Không được phép truy cập";
                    return View();
                }
                Session["TenTaiKhoan"] = TaiKhoan.FirstOrDefault().Username;
                if (TaiKhoan.First().Power == 1)
                {
                    Session["Quyen"] = "Admin";
                }
                if (TaiKhoan.First().Power == 2)
                {
                    Session["Quyen"] = "Employee";
                }
                return Redirect("/Home/Index"); ///đang fix
            }
            else
            {
                ViewBag.FailedMessage = "Thông tin tài khoản không chính xác!";
            }
            return View();
        }
        [HttpGet]
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
