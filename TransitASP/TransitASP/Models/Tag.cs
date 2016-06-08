namespace TransitASP.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Bus.Tags")]
    public partial class Tag
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Tag()
        {
            SpecialOnBoards = new HashSet<SpecialOnBoard>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TagId { get; set; }

        [Required]
        [StringLength(75)]
        public string Description { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? BeginDate { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? EndDate { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SpecialOnBoard> SpecialOnBoards { get; set; }
    }
}
