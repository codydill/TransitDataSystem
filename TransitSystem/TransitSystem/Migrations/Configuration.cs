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

            //Creating Tags
            var tags = new List<Tag>
            {
                new Tag { Description = "Adult", Current = true },
                new Tag { Description = "Youth", Current = true },
                new Tag { Description = "Pool Reduced", Current = false }
            };
            tags.ForEach(t => context.Tags.AddOrUpdate(d => d.Description, t));
            context.SaveChanges();


            //Creating Locations, Routes, and RouteDetails
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


            var station = context.Locations.Single(l => l.Name.Contains("Station"));

            var foodLocations = locations.FindAll(l => l.Name.Contains("Food"));
            foodLocations.Insert(0, station);
            AddLocationsToRoute(context, foodRoute, foodLocations);
            context.SaveChanges();

            var bankLocations = locations.FindAll(l => l.Name.Contains("Bank"));
            bankLocations.Insert(0, station);
            AddLocationsToRoute(context, bankRoute, bankLocations);
            context.SaveChanges();

            //Creating OnBoards

            //CreateOnBoardsForAllRoutes(context, DateTime.Today, 5);
            //context.SaveChanges();




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


        public OnBoard CreateOnBoard(TransitSystem.DAL.TransitContext context, Location loc, Route route, DateTime date)
        {
            var onBoard = new OnBoard { Location = loc, Route = route, OnBoardTimeStamp = date };
            context.OnBoards.Add(onBoard);
            return onBoard;
        }
        public void CreateOnBoardDetails(TransitSystem.DAL.TransitContext context, List<Tag> tags, OnBoard onBoard, Random rand)
        {
            foreach (var tag in tags)
            {
                context.OnBoardDetails.Add(new OnBoardDetail { Tag = tag, OnBoard = onBoard, Count = (byte)rand.Next(1, 2) });
            }
        }
        public void CreateOnBoardsForAllRoutes(TransitSystem.DAL.TransitContext context, DateTime date, double minutesBetweenLocations)
        {
            var rand = new Random(DateTime.Now.Millisecond);
            List<Tag> currentTags = context.Tags.Where(t => t.Current == true).ToList();
            List<Route> allRoutes = context.Routes.ToList();
            foreach (var route in allRoutes)
            {
                IEnumerable<Location> locations = route.RouteDetails.Select(r => r.Location);
                DateTime dateIncremented = date;
                foreach (var loc in locations)
                {
                    dateIncremented = dateIncremented.AddMinutes(minutesBetweenLocations);
                    var onBoard = CreateOnBoard(context, loc, route, dateIncremented);
                    CreateOnBoardDetails(context, currentTags, onBoard, rand);
                }

            }
        }
    }
}
