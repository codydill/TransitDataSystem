namespace TransitSystem.DAL
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using TransitSystem.Models;
    using System.Data.Entity.ModelConfiguration.Conventions;

    public partial class TransitContext : DbContext
    {
        public TransitContext()
            : base("TransitContext")
        {
        }

        public virtual DbSet<Location> Locations { get; set; }
        public virtual DbSet<OnBoard> OnBoards { get; set; }
        public virtual DbSet<Route> Routes { get; set; }
        public virtual DbSet<OnBoardDetail> OnBoardDetails { get; set; }
        public virtual DbSet<RouteDetail> RouteDetails { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
