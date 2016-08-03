using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
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
    public class RouteController : Controller
    {
        private TransitContext db = new TransitContext();

        // GET: Route
        public ActionResult Index(int? ID)
        {
            RouteIndexData viewModel = new RouteIndexData();
            viewModel.Routes = db.Routes.Include(i => i.RouteDetails.Select(r => r.Location));

            if (ID != null)
            {
                ViewBag.RouteId = ID.Value;
                viewModel.Locations = viewModel.Routes.Where(r => r.RouteID == ID.Value).Single()
                                    .RouteDetails.OrderBy(l => l.Position).Select(r => r.Location);
                
            }

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Index(int? id, string[] orderedLocations)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Route routeToUpdate = db.Routes.Include(r => r.RouteDetails).Where(r => r.RouteID == id).Single();
            if (TryUpdateModel(routeToUpdate, "", new string[] { "RouteName" }) && orderedLocations != null)
            {
                try
                {
                    UpdateRouteLocationOrder(routeToUpdate, orderedLocations);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (RetryLimitExceededException)
                {

                    ModelState.AddModelError("", "Unable to save.");
                }
            }
            return View(routeToUpdate);
        }

        private void UpdateRouteLocationOrder(Route route, string[] orderedLocations)
        {
            for (int i = 0; i < orderedLocations.Length; i++)
            {
                RouteDetail detailToUpdate = route.RouteDetails.Where(r => r.LocationID.ToString() == orderedLocations[i]).Single();
                detailToUpdate.Position = (byte)i;
            }
        }


        // GET: Route/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Route route = await db.Routes.FindAsync(id);
            if (route == null)
            {
                return HttpNotFound();
            }
            return View(route);
        }

        // GET: Route/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Route/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "RouteID,RouteName")] Route route)
        {
            if (ModelState.IsValid)
            {
                db.Routes.Add(route);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(route);
        }

        // GET: Route/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Route route = db.Routes.Include(r => r.RouteDetails).Where(r => r.RouteID == id).Single();
            PopulateAssignedLocationsData(route);
            if (route == null)
            {
                return HttpNotFound();
            }
            return View(route);
        }

        private void PopulateAssignedLocationsData(Route route)
        {
            var allLocations = db.Locations;
            var routeLocations = new HashSet<int>(route.RouteDetails.Select(l => l.LocationID));
            var viewModel = new List<AssignedLocationData>();
            foreach (var loc in allLocations)
            {
                viewModel.Add(new AssignedLocationData
                {
                    LocationId = loc.LocationID,
                    Name = loc.Name,
                    Assigned = routeLocations.Contains(loc.LocationID)
                });
            }
            ViewBag.Locations = viewModel;
        }

        // POST: Route/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? id, string[] selectedLocations)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Route routeToUpdate = db.Routes.Include(r => r.RouteDetails).Where(r => r.RouteID == id).Single();

            if (TryUpdateModel(routeToUpdate, "", new string[] { "RouteName" }))
            {
                try
                {
                    UpdateRouteLocations(routeToUpdate, selectedLocations);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (RetryLimitExceededException)
                {

                    ModelState.AddModelError("", "Unable to save.");
                }
            }
            PopulateAssignedLocationsData(routeToUpdate);
            return View(routeToUpdate);
        }

        private void UpdateRouteLocations(Route routeToUpdate, string[] selectedLocations)
        {
            if (selectedLocations == null)
            {
                foreach (var detail in routeToUpdate.RouteDetails.ToList())
                {
                    db.RouteDetails.Remove(detail);
                }
                return;
            }
            var selectedLocationsHS = new HashSet<string>(selectedLocations);
            var routeLocationsHS = new HashSet<int>(routeToUpdate.RouteDetails.Select(d => d.LocationID));
            foreach (var loc in db.Locations)
            {
                if (selectedLocationsHS.Contains(loc.LocationID.ToString()))
                {
                    if (!routeLocationsHS.Contains(loc.LocationID))
                    {
                        db.RouteDetails.Add(new RouteDetail
                            { LocationID = loc.LocationID, RouteID = routeToUpdate.RouteID, Position = 0 });
                    }
                }
                else
                {
                    if (routeLocationsHS.Contains(loc.LocationID))
                    {
                        var detailToRemove = routeToUpdate.RouteDetails.Where(d => d.LocationID == loc.LocationID && d.RouteID == routeToUpdate.RouteID).Single();
                        db.RouteDetails.Remove(detailToRemove);
                    }
                }
            }

        }

        // GET: Route/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Route route = await db.Routes.FindAsync(id);
            if (route == null)
            {
                return HttpNotFound();
            }
            return View(route);
        }

        // POST: Route/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Route route = db.Routes.Find(id);
            db.Routes.Remove(route);
            await db.SaveChangesAsync();
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
