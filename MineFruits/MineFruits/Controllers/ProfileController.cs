using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MineFruits.Models;
using Newtonsoft.Json.Linq;

namespace MineFruits.Controllers
{
    public class ProfileController : ProtectController
    {
        private MineFruitsDB db = new MineFruitsDB();

        // GET: Profile
        public ActionResult Index()
        {
            Account oldUser = Session["TaiKhoan"] as Account;
            Account newUser = db.Accounts.Where(us => us.Username.Equals(oldUser.Username)).FirstOrDefault();
            Session["TaiKhoan"] = newUser;
            var errMsg = TempData["ErrorMessage"] as string;
            ViewBag.Infor = errMsg;
            return View(newUser);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(FormCollection frm)
        {
            string tenTaiKhoan = frm["TenTaiKhoan"];
            string hoTen = frm["Name"];
            string soDienThoai = frm["PhoneNumber"];
            string Email = frm["Email"];
            string diaChi = frm["Address"];

            Account user = db.Accounts.Where(us => us.Username == tenTaiKhoan).SingleOrDefault();
            user.Name = hoTen;
            user.PhoneNumber = soDienThoai;
            user.Address = diaChi;
            user.Email = Email;
            if (user != null)
            {
                db.Entry(user).State = EntityState.Modified;
                db.Configuration.ValidateOnSaveEnabled = false;
                db.SaveChanges();
                ViewBag.Information = "Cập nhật thành công";
            }
            else
            {
                ViewBag.Information = "Có lỗi xảy ra khi cập nhật";
            }

            return View("Index", user);
        }

        public ActionResult ChangePass(string tenTK)
        {
            Account taiKhoan = db.Accounts.Find(tenTK);
            return View(taiKhoan);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePass([Bind(Include = "Username,Password,PhoneNumber,Email,Name,Address,Power,Status,CreateDate,UpdateDate")] Account taiKhoan,
            string OldPassword, string NewPassword)
        {
            string old_pass = db.Accounts.AsNoTracking().
                Where(t => t.Username.Equals(taiKhoan.Username)).
                FirstOrDefault().Password;
            if (old_pass.Equals(OldPassword))
            {
                if (ModelState.IsValid)
                {
                    if(NewPassword == OldPassword)
                    {
                        db.Entry(taiKhoan).State = EntityState.Modified;
                        db.SaveChanges();
                        TempData["message"] = "Thành công";
                        setAlert("Đổi mật khẩu thành công mời đăng nhập lại!", "success");
                        return RedirectToAction("Logout");
                    }
                    else
                    {
                        ViewBag.Message = "Xác nhận mật khẩu không trùng nhau !!!";
                    }

                }
                else
                {
                    ViewBag.Message = "Lỗi nhập dữ liệu!!!";
                }
            }
            else
            {
                ViewBag.Message = "Mật khẩu cũ không chính xác!!!";
            }
            return View(taiKhoan);
        }

        [NotAuthorize]
        public ActionResult Logout()
        {
            Session.Clear();
            //Session["TaiKhoan"] = null;
            //Session["Cartsession"] = null;
            return Redirect("/");
        }
        [NotAuthorize]
        public ActionResult Login()
        {

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [NotAuthorize]
        public ActionResult Login(string userName, string password)
        {
            if (string.IsNullOrEmpty(userName))
            {
                ViewBag.ErrorTenTaiKhoan = "Tên tài khoản không được để trống";
            }
            if (string.IsNullOrEmpty(password))
            {
                ViewBag.ErrorMatKhau = "Mật khẩu không được để trống";
            }
            var response = Request["g-Recaptcha-Response"];
            string secretKey = "6Lc6858dAAAAAFiGln-5wwd0CiKgyGW2eA6_j3H1";
            var client = new WebClient();
            var resultcaptcha = client.DownloadString(string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", secretKey, response));
            var obj = JObject.Parse(resultcaptcha);
            var status = (bool)obj.SelectToken("success");
            ViewBag.Message = status ? "" : "Xác thực thất bại";
            if (ModelState.IsValid && status)
            {
                var user = db.Accounts.Where(t => t.Username.Equals(userName) && t.Password.Equals(password) && t.Power == 0).ToList();
                if (user.Count() > 0)
                {

                    if (user.FirstOrDefault().Status == false)
                    {
                        // Hien thi thong bao loi
                        ViewBag.error = "Tài khoản bị khóa. Đăng nhập không thành công";
                    }
                    else
                    {
                        //Su dung session: add Session
                        Session["TaiKhoan"] = user.FirstOrDefault();
                        Session["TenKhachHang"] = user.FirstOrDefault().Name;
                        Session["TenTaiKhoan"] = user.FirstOrDefault().Username;
                        // Sang trang ch
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ViewBag.error = "Đăng nhập không thành công";
                }
            }
            return View();
        }

        [NotAuthorize]
        [HttpGet]
        public ActionResult Register()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [NotAuthorize]
        public ActionResult Register([Bind(Include = "Username,Password,PhoneNumber,Email,Name,Address,Power,Status,CreateDate,UpdateDate")] Account taiKhoan)
        {
            var response = Request["g-Recaptcha-Response"];
            string secretKey = "6Lc6858dAAAAAFiGln-5wwd0CiKgyGW2eA6_j3H1";
            var client = new WebClient();
            var resultcaptcha = client.DownloadString(string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", secretKey, response));
            var obj = JObject.Parse(resultcaptcha);
            var status = (bool)obj.SelectToken("success");
            ViewBag.Message = status ? "Xác thực thành công" : "Xác thực thất bại";
            if (ModelState.IsValid && status)
            {
                var taiKhoanFind = db.Accounts.Find(taiKhoan.Username);
                if (taiKhoanFind == null)
                {
                    db.Accounts.Add(taiKhoan);
                    db.SaveChanges();
                    return RedirectToAction("Login");
                }
                else
                {
                    ViewBag.ErrorSign = "Tên tài khoản trùng. Vui lòng nhập tên khác";
                }
            }
            //ViewBag.Infor = taiKhoan.ToString();
            return View(taiKhoan);
        }
    }
}
