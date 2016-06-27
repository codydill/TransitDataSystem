using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TransitSystem.DAL;
using TransitSystem.Models;
using TransitSystem.ViewModels;

namespace TransitSystem.Controllers
{
    public class OnBoardController : Controller
    {
        private TransitContext db = new TransitContext();

        // GET: OnBoard
        public ActionResult Index(int? ID)
        {
            OnBoardIndexData viewModel = new OnBoardIndexData();
            viewModel.CurrentTags = db.Tags.Where(t => t.Current == true);
            viewModel.Routes = db.Routes;

            if (ID != null)
            {
                viewModel.RouteLocations = viewModel.Routes.Where(r => r.RouteID == ID.Value).Single()
                                    .RouteDetails.OrderBy(l => l.Position).Select(r => r.Location);
            }
            //ViewBag.RouteId = new SelectList(viewModel.Routes, "RouteID", "RouteName", ID.Value);

            return View(viewModel);
        }

        // GET: OnBoard/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OnBoard onBoard = db.OnBoards.Find(id);
            if (onBoard == null)
            {
                return HttpNotFound();
            }
            return View(onBoard);
        }

        // GET: OnBoard/Create
        public ActionResult Create()
        {
            ViewBag.LocationID = new SelectList(db.Locations, "LocationID", "Name");
            ViewBag.RouteID = new SelectList(db.Routes, "RouteID", "RouteName");
            return View();
        }

        // POST: OnBoard/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OnBoardID,LocationID,RouteID,OnBoardTimeStamp")] OnBoard onBoard)
        {
            if (ModelState.IsValid)
            {
                db.OnBoards.Add(onBoard);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.LocationID = new SelectList(db.Locations, "LocationID", "Name", onBoard.LocationID);
            ViewBag.RouteID = new SelectList(db.Routes, "RouteID", "RouteName", onBoard.RouteID);
            return View(onBoard);
        }

        // GET: OnBoard/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OnBoard onBoard = db.OnBoards.Find(id);
            if (onBoard == null)
            {
                return HttpNotFound();
            }
            ViewBag.LocationID = new SelectList(db.Locations, "LocationID", "Name", onBoard.LocationID);
            ViewBag.RouteID = new SelectList(db.Routes, "RouteID", "RouteName", onBoard.RouteID);
            return View(onBoard);
        }

        // POST: OnBoard/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OnBoardID,LocationID,RouteID,OnBoardTimeStamp")] OnBoard onBoard)
        {
            if (ModelState.IsValid)
            {
                db.Entry(onBoard).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.LocationID = new SelectList(db.Locations, "LocationID", "Name", onBoard.LocationID);
            ViewBag.RouteID = new SelectList(db.Routes, "RouteID", "RouteName", onBoard.RouteID);
            return View(onBoard);
        }

        // GET: OnBoard/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OnBoard onBoard = db.OnBoards.Find(id);
            if (onBoard == null)
            {
                return HttpNotFound();
            }
            return View(onBoard);
        }

        // POST: OnBoard/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OnBoard onBoard = db.OnBoards.Find(id);
            db.OnBoards.Remove(onBoard);
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
