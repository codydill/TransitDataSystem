using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TransitSystem.Models;

namespace TransitSystem.ViewModels
{
    public class OnBoardRouteData
    {
        public Route SelectedRoute { get; set; }
        public IEnumerable<Tag> CurrentTags { get; set; }
        public IEnumerable<Location> RouteLocations { get; set; }
    }
}