//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TransitDataSystem
{
    using System;
    using System.Collections.Generic;
    
    public partial class SpecialOnBoard
    {
        public int DetailsID { get; set; }
        public int OnBoardID { get; set; }
        public int TagID { get; set; }
        public byte Count { get; set; }
    
        public virtual OnBoard OnBoard { get; set; }
        public virtual Tag Tag { get; set; }
    }
}
