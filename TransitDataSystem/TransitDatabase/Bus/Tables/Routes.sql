CREATE TABLE [Bus].[Routes] (
    [RouteID]   UNIQUEIDENTIFIER NOT NULL,
    [LocationID] UNIQUEIDENTIFIER NOT NULL,
	[LocationPositionInRoute] INT NOT NULL, 
    CONSTRAINT [PK_Bus] PRIMARY KEY CLUSTERED ([RouteID]), 
    CONSTRAINT [FK_LocationID_ToLocations] FOREIGN KEY (LocationID) REFERENCES Bus.[Locations](LocationID)
);

