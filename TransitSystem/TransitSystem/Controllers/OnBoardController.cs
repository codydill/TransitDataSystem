using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
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
        public async Task<ActionResult> Index()
        {
            return View(await db.Routes.ToListAsync());
        }


        // GET: OnBoard/Create
        public async Task<ActionResult> Create(int? ID)
        {
            if (ID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Not a Valid Route");
            }
            RouteDetailData viewModel = new RouteDetailData();

            Route currentRoute = db.Routes.Where(r => r.RouteID == ID.Value).Single();
            List<Tag> currentTags = db.Tags.Where(t => t.Current == true).ToList();
            List<Location> routeLocations = db.Routes.Where(r => r.RouteID == ID.Value).Single()
                                    .RouteDetails.OrderBy(l => l.Position).Select(r => r.Location).ToList();

            if (currentTags.Count == 0)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "At Least One Tag Must Be Current.");
            if (routeLocations.Count == 0)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Route Must Have At Least One Location.");

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
            await db.SaveChangesAsync();
            return View(viewModel);
        }

        // POST: OnBoard/Create
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(RouteDetailData routeData, string Command)
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
                            OnBoard updateOnBoard = await db.OnBoards.FindAsync(onBoardId);
                            updateOnBoard.OnBoardTimeStamp = DateTime.Now;
                            db.OnBoards.Attach(updateOnBoard);
                            db.Entry(updateOnBoard).Property(p => p.OnBoardTimeStamp).IsModified = true;
                            break;
                        }
                    }
                    await db.SaveChangesAsync();
                    ModelState.Clear();
                    return View(routeData);
                }
                else
                {
                    foreach (var group in routeData.Groups)
                    {
                        foreach (var detail in group.GroupDetails)
                        {
                            OnBoardDetail updateDetail = await db.OnBoardDetails.FindAsync(detail.DetailsID);
                            db.OnBoardDetails.Attach(updateDetail);
                            updateDetail.Count = detail.Count;
                            db.Entry(updateDetail).Property(d => d.Count).IsModified = true;
                        }
                    }

                }
                await db.SaveChangesAsync();
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
