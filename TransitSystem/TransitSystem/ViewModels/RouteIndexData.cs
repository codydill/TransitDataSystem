using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TransitSystem.Models;

namespace TransitSystem.ViewModels
{
    public class RouteIndexData
    {
        public IEnumerable<Route> Routes { get; set; }
        public IEnumerable<Location> Locations { get; set; }
    }
}