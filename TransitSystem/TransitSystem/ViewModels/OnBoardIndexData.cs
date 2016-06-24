using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TransitSystem.Models;

namespace TransitSystem.ViewModels
{
    public class OnBoardIndexData
    {
        public IEnumerable<Route> Routes { get; set; }
        public IEnumerable<Tag> CurrentTags { get; set; }
        public IEnumerable<Location> RouteLocations { get; set; }
    }
}