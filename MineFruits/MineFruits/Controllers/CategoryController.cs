﻿using MineFruits.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MineFruits.Controllers
{
    public class CategoryController : Controller
    {
        MineFruitsDB db = new MineFruitsDB();

        // GET: Category
        public ActionResult Detail(int id, string order, decimal? fromPrice, decimal? toPrice, int? page)
        {
            if (id <= 0)
            {
                return Redirect("/NotFound/Index");
            }
            var category = db.Categories.Find(id);
            if (category == null)
            {
                return Redirect("/NotFound/Index");
            }

            var products = db.Products.Where(p => p.CategoryID == id).OrderByDescending(p => p.ProductID);

            if (order != null)
                switch (order)
                {
                    case "desc":
                        products = products.OrderByDescending(p => p.Price);
                        ViewBag.order = "desc";
                        break;
                    case "asc":
                        products = products.OrderBy(p => p.Price);
                        ViewBag.order = "asc";
                        break;
                    default:
                        ViewBag.order = "default";
                        break;
                }

            if (fromPrice != null)
            {
                ViewBag.from = fromPrice;
                products = (IOrderedQueryable<Product>)products.Where(p => p.Price >= fromPrice);
            }

            if (toPrice != null)
            {
                ViewBag.to = toPrice;
                products = (IOrderedQueryable<Product>)products.Where(p => p.Price <= toPrice);
            }

            int pageNumber = (page ?? 1);
            ViewBag.Category = category.CategoryName;

            return View(products.ToPagedList(pageNumber, 9));
        }
    }
}