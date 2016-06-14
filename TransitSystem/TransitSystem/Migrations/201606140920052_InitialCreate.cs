namespace TransitSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Bus.Locations",
                c => new
                    {
                        LocationID = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 75),
                        Address = c.String(maxLength: 75),
                    })
                .PrimaryKey(t => t.LocationID);
            
            CreateTable(
                "Bus.OnBoards",
                c => new
                    {
                        OnBoardID = c.Int(nullable: false, identity: true),
                        LocationID = c.Int(nullable: false),
                        RouteID = c.Int(nullable: false),
                        OnBoardTimeStamp = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.OnBoardID)
                .ForeignKey("Bus.Locations", t => t.LocationID, cascadeDelete: true)
                .ForeignKey("Bus.Routes", t => t.RouteID, cascadeDelete: true)
                .Index(t => t.LocationID)
                .Index(t => t.RouteID);
            
            CreateTable(
                "Bus.OnBoardDetails",
                c => new
                    {
                        DetailsID = c.Int(nullable: false, identity: true),
                        OnBoardID = c.Int(nullable: false),
                        TagID = c.Int(nullable: false),
                        Count = c.Byte(nullable: false),
                    })
                .PrimaryKey(t => t.DetailsID)
                .ForeignKey("Bus.OnBoards", t => t.OnBoardID, cascadeDelete: true)
                .ForeignKey("Bus.Tags", t => t.TagID, cascadeDelete: true)
                .Index(t => t.OnBoardID)
                .Index(t => t.TagID);
            
            CreateTable(
                "Bus.Tags",
                c => new
                    {
                        TagId = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false, maxLength: 75),
                        Current = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.TagId);
            
            CreateTable(
                "Bus.Routes",
                c => new
                    {
                        RouteID = c.Int(nullable: false, identity: true),
                        RouteName = c.String(),
                    })
                .PrimaryKey(t => t.RouteID);
            
            CreateTable(
                "Bus.RouteDetails",
                c => new
                    {
                        RouteDetailID = c.Int(nullable: false, identity: true),
                        Position = c.Byte(nullable: false),
                        LocationID = c.Int(nullable: false),
                        RouteID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RouteDetailID)
                .ForeignKey("Bus.Locations", t => t.LocationID, cascadeDelete: true)
                .ForeignKey("Bus.Routes", t => t.RouteID, cascadeDelete: true)
                .Index(t => t.LocationID)
                .Index(t => t.RouteID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("Bus.RouteDetails", "RouteID", "Bus.Routes");
            DropForeignKey("Bus.RouteDetails", "LocationID", "Bus.Locations");
            DropForeignKey("Bus.OnBoards", "RouteID", "Bus.Routes");
            DropForeignKey("Bus.OnBoardDetails", "TagID", "Bus.Tags");
            DropForeignKey("Bus.OnBoardDetails", "OnBoardID", "Bus.OnBoards");
            DropForeignKey("Bus.OnBoards", "LocationID", "Bus.Locations");
            DropIndex("Bus.RouteDetails", new[] { "RouteID" });
            DropIndex("Bus.RouteDetails", new[] { "LocationID" });
            DropIndex("Bus.OnBoardDetails", new[] { "TagID" });
            DropIndex("Bus.OnBoardDetails", new[] { "OnBoardID" });
            DropIndex("Bus.OnBoards", new[] { "RouteID" });
            DropIndex("Bus.OnBoards", new[] { "LocationID" });
            DropTable("Bus.RouteDetails");
            DropTable("Bus.Routes");
            DropTable("Bus.Tags");
            DropTable("Bus.OnBoardDetails");
            DropTable("Bus.OnBoards");
            DropTable("Bus.Locations");
        }
    }
}
