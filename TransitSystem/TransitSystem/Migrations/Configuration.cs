namespace TransitSystem.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using TransitSystem.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<TransitSystem.DAL.TransitContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(TransitSystem.DAL.TransitContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            var tags = new List<Tag>
            {
                new Tag { Description = "Adult", Current = true },
                new Tag { Description = "Youth", Current = true },
                new Tag { Description = "Pool Reduced", Current = false }
            };
            tags.ForEach(t => context.Tags.AddOrUpdate(d => d.Description, t));
            context.SaveChanges();

            var locations = new List<Location>
            {
                new Location { Name = "Transit Station", Address = "North 6th Avenue" },
                new Location { Name = "Walmart - Food", Address = "South 16th Street" },
                new Location { Name = "Hospital - Medical", Address = "Bend Avenue" },
                new Location { Name = "Safeway - Food", Address = "South 1st Street" },
                new Location { Name = "Mall - Food", Address = "Cherry Lane" },
                new Location { Name = "Tree Bank", Address = "Baker Street" },
                new Location { Name = "Doughnut Shop Food", Address = "Pine Street" },
                new Location { Name = "Bank of America", Address = "Lincoln Street" },
                new Location { Name = "Bank of Africa", Address = "North 4th Street" }
            };
            locations.ForEach(l => context.Locations.AddOrUpdate(d => d.Name, l));
            context.SaveChanges();

            var foodRoute = new Route { RouteName = "Food Route" };
            var bankRoute = new Route { RouteName = "Bank Route" };
            var medicalRoute = new Route { RouteName = "Medical Route" };
            var routes = new List<Route> { foodRoute, bankRoute, medicalRoute };
            routes.ForEach(r => context.Routes.AddOrUpdate(d => d.RouteName, r));
            context.SaveChanges();

            var foodLocations = locations.FindAll(l => l.Name.Contains("Food"));
            AddLocationsToRoute(context, foodRoute, foodLocations);
            context.SaveChanges();

            var bankLocations = locations.FindAll(l => l.Name.Contains("Bank"));
            AddLocationsToRoute(context, bankRoute, bankLocations);
            context.SaveChanges();







            //context.Dispose();
            //context = new MyDbContext();
            //context.Configuration.AutoDetectChangesEnabled = false;
            //context.Configuration.AutoDetectChangesEnabled = false;
            //context.Configuration.ValidateOnSaveEnabled = false;
        }
        public void AddLocationsToRoute(TransitSystem.DAL.TransitContext context, Route route, List<Location> locations)
        {
            byte i = 0;
            foreach (var loc in locations)
            {
                context.RouteDetails.AddOrUpdate(new RouteDetail { RouteID = route.RouteID, LocationID = loc.LocationID, Position = i++ });
            }
        }
        //public void AddLocations(IEnumerable<Location> locations)
        //{
        //    byte i = 0;
        //    foreach (var loc in locations)
        //    {
        //        RouteDetails.Add(new RouteDetail { RouteID = RouteID, LocationID = loc.LocationID, Position = i++ });
        //    }
        //}

        //public void RemoveLocations()
        //{
        //    RouteDetail[] details = new RouteDetail[RouteDetails.Count];
        //    RouteDetails.CopyTo(details, 0);
        //    foreach (var detail in details)
        //    {
        //        RouteDetails.Remove(detail);
        //    }
        //}
    }
}
