using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OnlineStore.DATA.EF;
using OnlineStore.UI.MVC.Models;

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

        //Below we create an action that will return a grid layout view of the beer
        public ActionResult BeerGrid()
        {
            var beers = db.Products.Include(b => b.BeerStyleID).Include(b => b.BeerStyle).Include(b => b.BeerName).Include(b => b.ABV).Include(b => b.BeerImage).Include(b => b.BeerDesc);
            return View(beers.ToList());
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

        #region Custom Add-To-Cart Functionality
        public ActionResult AddToCart(int qty, int beerID)
        {
            //Create an empty shell for LOCAL shopping cart variable
            Dictionary<int, CartItemViewModel> shoppingCart = null;

            //Session-based variables are used to manage the state of functionality in our application. In this case, we will store/use the information from a session-based variable called cart. Below, we are checking to see if session["cart"] exists...if so, use it to populate the shoppingCart (local variable)
            if (Session["cart"] != null)
            {
                //Session["cart"] exists and we will put its items in the local shoppingCart
                shoppingCart = (Dictionary<int, CartItemViewModel>)Session["cart"];
                //With Session variables, think aout the process of BOXING and UNBOXING from ArrayLists. Session stores the data in an object format/datatype, so here we must UNBOX that information to its original datatype so we can use it in the logic in our action.
            }
            else
            {
                //shopping cart doesn't exist, we need to instantiate/create it
                shoppingCart = new Dictionary<int, CartItemViewModel>();
            }//After this if/else, we now have an accurate picture of the shoppingCart and are ready to add things to it.

            //find the product in the db by its id
            Product product = db.Products.Where(b => b.BeerID == beerID).FirstOrDefault();
            if (product == null)
            {
                //if bad ID, kick them back to the main view of products
                return RedirectToAction("Index");
            }
            else
            {
                //if beers is valid, add the line-item to the cart
                CartItemViewModel item = new CartItemViewModel(qty, product);

                //put the item into the cart BUT if we already have the product as a cart-item, then update the qty instead. This is why we have the dictionary.
                if (shoppingCart.ContainsKey(product.BeerID))
                {
                    shoppingCart[product.BeerID].Qty += qty;//Here we access the quantity for that line item and add the number of items they selected.
                }
                else
                {
                    shoppingCart.Add(product.BeerID, item);//add the item to the dictionary
                }

                //We have updated our local objects, and now we need to update the Session["cart"]
                Session["cart"] = shoppingCart;//NO EXPLICIT CASTING needed to add this item to the shoppingCart as the shoppingCart is being impllicitly cast into a larger container
            }

            //send the user to the shoppingCart view to see the cart items
            return RedirectToAction("Index", "ShoppingCart");

        }
        #endregion





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
