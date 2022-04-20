using MineFruits.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MineFruits.Controllers
{
    public class ContactController : Controller
    {
        // GET: Contact
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Send(string name, string phone, string email, string address, string content)
        {
            var feedBack = new Feedback();
            feedBack.Name = name;
            feedBack.PhoneNumber = phone;
            feedBack.Email = email;
            feedBack.CreateDate = DateTime.Now;
            feedBack.Address = address;
            feedBack.Content = content;

            var id = new ContactDao().InsertFeeBack(feedBack);
            if (id > 0)
                return Json(new
                {
                    status = true
                });
            else
                return Json(new
                {
                    status = false
                });
        }
    }
}