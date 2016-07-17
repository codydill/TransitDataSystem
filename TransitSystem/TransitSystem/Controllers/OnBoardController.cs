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

            List<OnBoard> onBoardsToAdd = new List<OnBoard>();
            foreach (var loc in routeLocations)
            {
                OnBoard onBoard = new OnBoard() { Location = loc, Route = currentRoute };
                db.OnBoards.Add(onBoard);
                onBoardsToAdd.Add(onBoard);
                DetailGroup detailGroup = new DetailGroup() { GroupLocation = loc, GroupDetails = new List<OnBoardDetail>()};
                viewModel.Groups.Add(detailGroup);
                foreach (var tag in currentTags)
                {
                    OnBoardDetail detail = new OnBoardDetail() { Tag = tag, Count = 0, OnBoard = onBoard };
                    db.OnBoardDetails.Add(detail);
                    detailGroup.GroupDetails.Add(detail);
                }
            }
            for (int i = 0; i < viewModel.Groups.Count; i++)
            {
                viewModel.Groups[i].OnBoardItem = onBoardsToAdd[i];
            }
            db.SaveChanges();
            return View(viewModel);
        }

        // POST: OnBoard/Create
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create(RouteDetailData routeData, string Command)
        {
            if (ModelState.IsValid)
            {
                if (Command != "Save and Reset")
                {
                    int onBoardId = int.Parse(Command);
                    for (int i = 0; i < routeData.Groups.Count; i++)
                    {
                        DetailGroup groupItem = routeData.Groups[i];
                        if (groupItem.OnBoardItem.OnBoardID == onBoardId)
                        {
                            routeData.ActiveGroupIndex = (i + 1) % routeData.Groups.Count;
                            groupItem.IsSet = true;
                            OnBoard updateOnBoard = db.OnBoards.Find(onBoardId);
                            updateOnBoard.OnBoardTimeStamp = DateTime.Now;
                            db.OnBoards.Attach(updateOnBoard);
                            db.Entry(updateOnBoard).Property(p => p.OnBoardTimeStamp).IsModified = true;
                            break;
                        }
                    }
                    db.SaveChanges();
                    ModelState.Clear();
                    return View(routeData);
                }
                else
                {
                    foreach (var group in routeData.Groups)
                    {
                        foreach (var detail in group.GroupDetails)
                        {
                            OnBoardDetail updateDetail = db.OnBoardDetails.Find(detail.DetailsID);
                            db.OnBoardDetails.Attach(updateDetail);
                            updateDetail.Count = detail.Count;
                            db.Entry(updateDetail).Property(d => d.Count).IsModified = true;
                        }
                    }

                }
                db.SaveChanges();
            }
            return RedirectToAction("Create", routeData.SelectedRoute.RouteID);
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
