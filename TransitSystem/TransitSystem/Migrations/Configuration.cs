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
                new Location { Name = "Walmart", Address = "South 16th Street" },
                new Location { Name = "Hospital", Address = "Bend Avenue" },
                new Location { Name = "Safeway", Address = "South 1st Street" },
                new Location { Name = "Mall", Address = "Cherry Lane" },
                new Location { Name = "Money Tree", Address = "Baker Street" },
                new Location { Name = "Doughnut Shop", Address = "Pine Street" },
                new Location { Name = "Bank of America", Address = "Lincoln Street" },
                new Location { Name = "Bank of Africa", Address = "North 4th Street" }
            };
            locations.ForEach(l => context.Locations.AddOrUpdate(d => d.Name, l));
            context.SaveChanges();

            var routes = new List<Route>
            {
                new Route { RouteID = 5, RouteName = "Food Route" },
                new Route { RouteID = 7, RouteName = "Monetary Route" },
                new Route { RouteID = 11, RouteName = "Medical Route" }
            };
            routes.ForEach(r => context.Routes.AddOrUpdate(d => d.RouteName, r));
            context.SaveChanges();

            var routeDetails = new List<RouteDetail>
            {
                new RouteDetail { Position = 0,
                    LocationID = 1,
                    RouteID = 5}
            };
            routeDetails.ForEach(r => context.RouteDetails.AddOrUpdate(d => d.RouteDetailID, r));
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
    }
}
