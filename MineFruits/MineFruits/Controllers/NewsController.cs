using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MineFruits.Models;
using PagedList;

namespace MineFruits.Controllers
{
    public class NewsController : Controller
    {
        private MineFruitsDB db = new MineFruitsDB();

        // GET: News
        public ActionResult Index(int? page)
        {
            var tinTucs = db.News.Select(t => t);
            tinTucs = db.News.OrderByDescending(t => t.UpdateDate);
            int pageNumber = (page ?? 1);

            return View(tinTucs.ToPagedList(pageNumber, 5));
        }

        // GET: News/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News tinTuc = db.News.Find(id);
            if (tinTuc == null)
            {
                return HttpNotFound();
            }
            return View(tinTuc);
        }
    }
}
