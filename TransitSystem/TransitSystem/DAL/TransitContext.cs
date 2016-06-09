namespace TransitSystem.DAL
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using TransitSystem.Models;

    public partial class TransitContext : DbContext
    {
        public TransitContext()
            : base("TransitContext")
        {
        }

        public virtual DbSet<Bus> Buses { get; set; }
        public virtual DbSet<Location> Locations { get; set; }
        public virtual DbSet<OnBoard> OnBoards { get; set; }
        public virtual DbSet<Route> Routes { get; set; }
        public virtual DbSet<SpecialOnBoard> SpecialOnBoards { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bus>()
                .HasMany(e => e.OnBoards)
                .WithRequired(e => e.Bus)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Location>()
                .Property(e => e.Address)
                .IsUnicode(false);

            modelBuilder.Entity<Location>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Location>()
                .HasMany(e => e.OnBoards)
                .WithRequired(e => e.Location)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Location>()
                .HasMany(e => e.Routes)
                .WithRequired(e => e.Location)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<OnBoard>()
                .HasMany(e => e.SpecialOnBoards)
                .WithRequired(e => e.OnBoard)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Route>()
                .HasMany(e => e.Buses)
                .WithRequired(e => e.Route)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Tag>()
                .Property(e => e.Description)
                .IsFixedLength();

            modelBuilder.Entity<Tag>()
                .HasMany(e => e.SpecialOnBoards)
                .WithRequired(e => e.Tag)
                .WillCascadeOnDelete(false);
        }
    }
}
