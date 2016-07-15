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
            RouteDetailData viewModel = new RouteDetailData();

            Route currentRoute = db.Routes.Where(r => r.RouteID == ID.Value).Single();
            List<Tag> currentTags = db.Tags.Where(t => t.Current == true).ToList();
            List<Location> routeLocations = db.Routes.Where(r => r.RouteID == ID.Value).Single()
                                    .RouteDetails.OrderBy(l => l.Position).Select(r => r.Location).ToList();

            viewModel.SelectedRoute = currentRoute;
            viewModel.Groups = new List<DetailGroup>();
            foreach (var loc in routeLocations)
            {
                OnBoard onBoard = new OnBoard() { Location = loc, Route = currentRoute };
                db.OnBoards.Add(onBoard);
                //add onboard to viewmodel.
                DetailGroup detailGroup = new DetailGroup() { GroupLocation = loc, GroupDetails = new List<OnBoardDetail>()};
                viewModel.Groups.Add(detailGroup);
                foreach (var tag in currentTags)
                {
                    OnBoardDetail detail = new OnBoardDetail() { Tag = tag, Count = 0, OnBoard = onBoard };
                    db.OnBoardDetails.Add(detail);
                    detailGroup.GroupDetails.Add(detail);
                }
            }
            db.SaveChanges();
            return View(viewModel);
        }

        // POST: OnBoard/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create(RouteDetailData routeData)
        {
            //OnBoardDetail det = new OnBoardDetail();
            //db.OnBoardDetails.Attach(det);
            //db.Entry(det).Property(d => d.Count).IsModified = true;
            //db.SaveChanges();

            if (ModelState.IsValid)
            {
                foreach (var group in routeData.Groups)
                {
                    foreach (var detail in group.GroupDetails)
                    {
                        db.OnBoardDetails.Attach(detail);
                        db.Entry(detail).Property(d => d.Count).IsModified = true;
                    }
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(routeData);
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
