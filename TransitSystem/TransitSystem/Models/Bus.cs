namespace TransitSystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Bus.Buses")]
    public partial class Bus
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Bus()
        {
            OnBoards = new HashSet<OnBoard>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int BusID { get; set; }

        public string BusName { get; set; }

        public string RouteName { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OnBoard> OnBoards { get; set; }

        public virtual Route Route { get; set; }
    }
}
