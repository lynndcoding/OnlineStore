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
    public class BeerStylesController : Controller
    {
        private OnlineStoreEntities db = new OnlineStoreEntities();

        // GET: BeerStyles
        public ActionResult Index()
        {
            return View(db.BeerStyles.ToList());
        }

        // GET: BeerStyles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BeerStyle beerStyle = db.BeerStyles.Find(id);
            if (beerStyle == null)
            {
                return HttpNotFound();
            }
            return View(beerStyle);
        }

        // GET: BeerStyles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BeerStyles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BeerStyleID,BeerStyle1,BeerDesc")] BeerStyle beerStyle)
        {
            if (ModelState.IsValid)
            {
                db.BeerStyles.Add(beerStyle);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(beerStyle);
        }

        // GET: BeerStyles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BeerStyle beerStyle = db.BeerStyles.Find(id);
            if (beerStyle == null)
            {
                return HttpNotFound();
            }
            return View(beerStyle);
        }

        // POST: BeerStyles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BeerStyleID,BeerStyle1,BeerDesc")] BeerStyle beerStyle)
        {
            if (ModelState.IsValid)
            {
                db.Entry(beerStyle).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(beerStyle);
        }

        // GET: BeerStyles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BeerStyle beerStyle = db.BeerStyles.Find(id);
            if (beerStyle == null)
            {
                return HttpNotFound();
            }
            return View(beerStyle);
        }

        // POST: BeerStyles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BeerStyle beerStyle = db.BeerStyles.Find(id);
            db.BeerStyles.Remove(beerStyle);
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
