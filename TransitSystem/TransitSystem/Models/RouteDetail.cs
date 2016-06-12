namespace TransitSystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Bus.RouteDetails")]
    public partial class RouteDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RouteDetailID { get; set; }

        public byte Position { get; set; }

        public int LocationID { get; set; }

        public int RouteID { get; set; }

        public virtual Location Location { get; set; }

        public virtual Route Route { get; set; }
    }
}