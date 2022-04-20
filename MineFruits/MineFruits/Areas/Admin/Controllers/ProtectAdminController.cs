using MineFruits.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MineFruits.Areas.Admin.Controllers
{
    public class ProtectAdminController : Controller
    {
        // GET: Admin/ProtectAdmin
        public Account user
        {
            get
            {
                Account _user = Session["TenTaiKhoan"] as Account;
                if (_user == null)
                    _user = new Account();
                return _user;
            }
        }

        public class NotAuthorizeAttribute : FilterAttribute { }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            object[] attributes = filterContext.ActionDescriptor.GetCustomAttributes(true);
            if (attributes.Any(a => a is NotAuthorizeAttribute)) return;
            if (Session["TenTaiKhoan"] == null)
            {
                filterContext.Result = new RedirectResult("/Admin/Auth/Login");
            }
            base.OnActionExecuting(filterContext);
        }

        protected void setAlert(string message, string type)
        {
            TempData["AlertMessage"] = message;
            if (type == "success")
            {
                TempData["AlertType"] = "alert-success";
            }
            else if (type == "warning")
            {
                TempData["AlertType"] = "alert-warning";
            }
            else
            {
                TempData["AlertType"] = "alert-danger";
            }
        }
    }
}