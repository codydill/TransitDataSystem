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
        public ActionResult Index()
        {
            return View(db.Routes.ToList());
        }


        // GET: OnBoard/Create
        public ActionResult Create(int? ID)
        {
            if (ID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OnBoardRouteData viewModel = new OnBoardRouteData();
            viewModel.SelectedRoute = db.Routes.Where(r => r.RouteID == ID.Value).Single();
            viewModel.CurrentTags = db.Tags.Where(t => t.Current == true);
            viewModel.RouteLocations = db.Routes.Where(r => r.RouteID == ID.Value).Single()
                                    .RouteDetails.OrderBy(l => l.Position).Select(r => r.Location);
            return View(viewModel);
        }

        // POST: OnBoard/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
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
