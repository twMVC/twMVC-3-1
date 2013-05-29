using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcApplication1.Models;
using MvcApplication1.Services;

namespace MvcApplication1.Controllers
{
    [FillQueryStringToRouteDataValues]
    public class ProductsController : Controller
    {
        //private NorthwindEntities db = new NorthwindEntities();

        ProductsService service = new ProductsService();

        //
        // GET: /Products/

        public ViewResult Index(string page, SearchModel searchModel)
        {
            //for search
            ViewBag.SearchModel = searchModel;
            service.SearchModel = searchModel;

            //for pager
            int currentPage = 0;
            int.TryParse(page, out currentPage);
            currentPage = currentPage - 1 < 0 ? 0 : currentPage - 1;
            var pager = new PagerModel()
            {
                PageSize = service.PageSize,
                CurrentPage = currentPage + 1,
                TotalItemCount = service.TotalItemCount
            };
            ViewBag.Pager = pager;

            service.CurrentPage = currentPage;
            var products = service.GetList();

            return View(products);
        }



        //public ViewResult Index(string page)
        //{
        //    //for pager
        //    int currentPage = 0;
        //    int.TryParse(page, out currentPage);
        //    currentPage = currentPage - 1 < 0 ? 0 : currentPage - 1;
        //    var pager = new PagerModel()
        //    {
        //        PageSize = service.PageSize,
        //        CurrentPage = currentPage + 1,
        //        TotalItemCount = service.TotalItemCount
        //    };
        //    ViewBag.Pager = pager;

        //    service.CurrentPage = currentPage;
        //    var products = service.GetList();

        //    return View(products);
        //}


        //public ViewResult Index()
        //{
        //    var products = service.GetList();
        //    return View(products);
        //}

        //
        // GET: /Products/Details/5

        public ViewResult Details(int id)
        {
            Products products = service.GetSingle(id);
            return View(products);
        }

        //
        // GET: /Products/Create

        public ActionResult Create()
        {
            ViewBag.CategoryID = new SelectList(service.GetCategoryList(), "CategoryID", "CategoryName");
            ViewBag.SupplierID = new SelectList(service.GetSupplierList(), "SupplierID", "CompanyName");
            return View();
        }

        //
        // POST: /Products/Create

        [HttpPost]
        public ActionResult Create(Products products)
        {
            if (ModelState.IsValid)
            {
                service.Create(products);
                return RedirectToAction("Index",ControllerContext.RouteData.Values);
            }

            ViewBag.CategoryID = new SelectList(service.GetCategoryList(), "CategoryID", "CategoryName", products.CategoryID);
            ViewBag.SupplierID = new SelectList(service.GetSupplierList(), "SupplierID", "CompanyName", products.SupplierID);
            return View(products);
        }

        //
        // GET: /Products/Edit/5

        public ActionResult Edit(int id)
        {
            Products products = service.GetSingle(id);
            ViewBag.CategoryID = new SelectList(service.GetCategoryList(), "CategoryID", "CategoryName", products.CategoryID);
            ViewBag.SupplierID = new SelectList(service.GetSupplierList(), "SupplierID", "CompanyName", products.SupplierID);
            return View(products);
        }

        //
        // POST: /Products/Edit/5

        [HttpPost]
        public ActionResult Edit(Products products)
        {
            if (ModelState.IsValid)
            {
                service.Edit(products);
                return RedirectToAction("Index", ControllerContext.RouteData.Values);
            }
            ViewBag.CategoryID = new SelectList(service.GetCategoryList(), "CategoryID", "CategoryName", products.CategoryID);
            ViewBag.SupplierID = new SelectList(service.GetSupplierList(), "SupplierID", "CompanyName", products.SupplierID);
            return View(products);
        }

        //
        // GET: /Products/Delete/5

        public ActionResult Delete(int id)
        {
            Products products = service.GetSingle(id);
            return View(products);
        }

        //
        // POST: /Products/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            service.Delete(id);
            return RedirectToAction("Index", ControllerContext.RouteData.Values);
        }

        protected override void Dispose(bool disposing)
        {
            service.Dispose();
            base.Dispose(disposing);
        }
    }


}