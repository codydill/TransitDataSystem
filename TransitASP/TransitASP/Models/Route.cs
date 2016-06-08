namespace TransitASP.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Bus.Routes")]
    public partial class Route
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Route()
        {
            Buses = new HashSet<Bus>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int RouteID { get; set; }

        public string RouteName { get;  set;}

        public int LocationID { get; set; }

        public int? LocationPositionInRoute { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Bus> Buses { get; set; }

        public virtual Location Location { get; set; }
    }
}
