using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OnlineStore.DATA.EF;

namespace OnlineStore.UI.MVC.Controllers
{
    public class ProductsController : Controller
    {
        private OnlineStoreEntities db = new OnlineStoreEntities();

        // GET: Products
        public ActionResult Index()
        {
            var products = db.Products.Include(p => p.BeerStyle).Include(p => p.Package).Include(p => p.Status);
            return View(products.ToList());
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
            ViewBag.BeerStyleID = new SelectList(db.BeerStyles, "BeerStyleID", "BeerStyle1");
            ViewBag.PkgID = new SelectList(db.Packages, "PkgID", "PkgID");
            ViewBag.StatusID = new SelectList(db.Statuses, "StatusID", "Status1");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BeerID,BeerStyleID,BeerName,BeerDesc,ABV,BeerImage,StatusID,PkgID")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BeerStyleID = new SelectList(db.BeerStyles, "BeerStyleID", "BeerStyle1", product.BeerStyleID);
            ViewBag.PkgID = new SelectList(db.Packages, "PkgID", "PkgID", product.PkgID);
            ViewBag.StatusID = new SelectList(db.Statuses, "StatusID", "Status1", product.StatusID);
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
            ViewBag.BeerStyleID = new SelectList(db.BeerStyles, "BeerStyleID", "BeerStyle1", product.BeerStyleID);
            ViewBag.PkgID = new SelectList(db.Packages, "PkgID", "PkgID", product.PkgID);
            ViewBag.StatusID = new SelectList(db.Statuses, "StatusID", "Status1", product.StatusID);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BeerID,BeerStyleID,BeerName,BeerDesc,ABV,BeerImage,StatusID,PkgID")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BeerStyleID = new SelectList(db.BeerStyles, "BeerStyleID", "BeerStyle1", product.BeerStyleID);
            ViewBag.PkgID = new SelectList(db.Packages, "PkgID", "PkgID", product.PkgID);
            ViewBag.StatusID = new SelectList(db.Statuses, "StatusID", "Status1", product.StatusID);
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
    }
}
