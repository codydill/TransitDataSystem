using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using TransitSystem.Models;

namespace TransitSystem.DAL
{
    public class TransitInitializer : System.Data.Entity.DropCreateDatabaseAlways<TransitContext>
    {
        protected override void Seed(TransitContext context)
        {
            var tags = new List<Tag>
            {
                new Tag { Description = "Adult", Current = true },
                new Tag { Description = "Youth", Current = true },
                new Tag { Description = "Pool Reduced", Current = false }
            };
            tags.ForEach(t => context.Tags.Add(t));
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
            locations.ForEach(l => context.Locations.Add(l));
            context.SaveChanges();

        }
    }
}