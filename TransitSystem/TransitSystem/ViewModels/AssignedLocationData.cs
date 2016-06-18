using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TransitSystem.ViewModels
{
    public class AssignedLocationData
    {
        public int LocationId { get; set; }
        public string Name { get; set; }
        public bool Assigned { get; set; }
    }
}