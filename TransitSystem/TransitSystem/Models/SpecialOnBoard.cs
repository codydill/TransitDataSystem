namespace TransitSystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Bus.SpecialOnBoard")]
    public partial class SpecialOnBoard
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DetailsID { get; set; }

        public int OnBoardID { get; set; }

        public int TagID { get; set; }

        public byte Count { get; set; }

        public virtual OnBoard OnBoard { get; set; }

        public virtual Tag Tag { get; set; }
    }
}
