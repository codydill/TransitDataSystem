﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TransitSystem.Models;

namespace TransitSystem.ViewModels
{
    public class RouteDetailData
    {
        public Route SelectedRoute { get; set; }
        public int ActiveGroupIndex { get; set; }
        public IList<DetailGroup> Groups { get; set; }
    }

    public class DetailGroup
    {
        public Location GroupLocation { get; set; }
        public OnBoard OnBoardItem { get; set; }
        public IList<OnBoardDetail> GroupDetails { get; set; }
    }
}