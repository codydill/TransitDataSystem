namespace TransitSystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Bus.OnBoards")]
    public partial class OnBoard
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public OnBoard()
        {
            OnBoardDetail = new HashSet<OnBoardDetail>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OnBoardID { get; set; }

        public int LocationID { get; set; }

        public int RouteID { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime OnBoardTimeStamp { get; set; }

        public virtual Route Route { get; set; }

        public virtual Location Location { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OnBoardDetail> OnBoardDetail { get; set; }
    }
}
