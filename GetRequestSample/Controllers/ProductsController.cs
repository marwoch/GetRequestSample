using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GetRequestSample.Models;

namespace GetRequestSample.Controllers
{
    public class ProductsController : Controller
    {
        private ProductDB db = new ProductDB();

        // GET: Products
        public ActionResult Index()
        {
            return View(db.Products.ToList());
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductId,Category,Name,Price")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductId,Category,Name,Price")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        [HttpGet]
        public ActionResult Search(SearchResults results)
        {
            bool isSearch = false;
            ProductDB db = new ProductDB();
            var prods =
                from p in db.Products
                select p;
            if (!string.IsNullOrWhiteSpace(results.Name))
            {
                prods = from prod in prods
                        where prod.Name.Contains(results.Name)
                        select prod;
                isSearch = true;
            }
            if (!string.IsNullOrWhiteSpace(results.Category))
            {
                prods = from prod in prods
                        where prod.Category == results.Category
                        select prod;
            }
            if (results.MinimumPrice.HasValue)
            {
                prods = from prod in prods
                        where prod.Price >= results.MinimumPrice
                        select prod;
            }
            if (results.MaximumPrice.HasValue)
            {
                prods = from prod in prods
                        where prod.Price <= results.MaximumPrice
                        select prod;
            }
            if (isSearch)
            {
                results.Products = prods.ToList();
            }
            return View(results);
        }

        //simple search with plain html form
        [HttpGet]
        private ActionResult BasicSearch()
        {
            string name = Request.QueryString["prodName"];
            if (!string.IsNullOrWhiteSpace(name))
            {
                ProductDB db = new ProductDB();
                List<Product> prods = (from p in db.Products
                                       where p.Name == name
                                       select p).ToList();
                //return view and pass in prods
                //if usin modle binding
                return View();
            }
            return View();
        } 
    }
}
