using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MvcApplication1.Models;
using System.Data;

namespace MvcApplication1.Services
{
    public class ProductsService
    {
        private NorthwindEntities db = new NorthwindEntities();

        //for dropdown list
        public List<Categories> GetCategoryList()
        {
            return db.Categories.ToList();
        }
        public List<Suppliers> GetSupplierList()
        {
            return db.Suppliers.ToList();
        }

        //
        // GET: /Products/

        //for pager
        public int PageSize
        {
            get
            {
                return 8;
            }
        }
        public int TotalItemCount
        {
            get
            {
                return GetAll().Count();
            }
        }
        public int CurrentPage { get; set; }

        public List<Products> GetList()
        {
            return GetAll()
                            .OrderBy(a => a.ProductID)
                //.OrderByDescending(a => a.ProductID)
                            .Skip(PageSize * CurrentPage)
                            .Take(PageSize)
                            .ToList();
        }

        public SearchModel SearchModel { get; set; }

        private IQueryable<Products> GetAll()
        {
            var data = db.Products.Include("Categories").Include("Suppliers").AsQueryable();

            //for Search
            if (!string.IsNullOrEmpty(SearchModel.ProductName))
                data = data.Where(a => a.ProductName.Contains(SearchModel.ProductName));
            if (!string.IsNullOrEmpty(SearchModel.CategoryName))
                data = data.Where(a => a.Categories.CategoryName.Contains(SearchModel.CategoryName));

            return data;
        }


        //private IQueryable<Products> GetAll()
        //{
        //    var data = db.Products.Include("Categories").Include("Suppliers").AsQueryable();
        //    return data;
        //}


        //public List<Products> GetList()
        //{
        //    var products = db.Products.Include("Categories").Include("Suppliers");
        //    return products.ToList();
        //}

        //
        // GET: /Products/Details/5

        public Products GetSingle(int id)
        {
            Products products = db.Products.Single(p => p.ProductID == id);
            return products;
        }

        //
        // POST: /Products/Create

        public void Create(Products products)
        {
            db.Products.AddObject(products);
            db.SaveChanges();
        }

        //
        // POST: /Products/Edit/5

        public void Edit(Products products)
        {
            db.Products.Attach(products);
            db.ObjectStateManager.ChangeObjectState(products, EntityState.Modified);
            db.SaveChanges();
        }

        //
        // POST: /Products/Delete/5

        public void Delete(int id)
        {
            Products products = db.Products.Single(p => p.ProductID == id);
            db.Products.DeleteObject(products);
            db.SaveChanges();
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }


}